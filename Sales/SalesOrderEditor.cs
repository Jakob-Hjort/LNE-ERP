using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderEditor : Screen
    {
        public override string Title { get; set; } = "Sales Order";
        SalesOrder salesorder = new();

        public SalesOrderEditor(SalesOrder salesOrder)
        {
            Title = "Redigerer for " + salesOrder.SalesOrderName;
            this.salesorder = salesOrder;
        }

        protected override void Draw()
        {
            ExitOnEscape();
            Form<SalesOrder> form = new();

            //form.TextBox("Sales order ID", nameof(SalesOrder.SalesOrderId));
            form.TextBox("Sales order Name", nameof(SalesOrder.SalesOrderName));
            form.TextBox("CustomerID", nameof(SalesOrder.CustomerId));
            form.TextBox("Date", nameof(SalesOrder.SalesOrderDate));
            form.TextBox("Price", nameof(SalesOrder.Prices));
            if (form.Edit(salesorder))
            {
                if (salesorder.SalesOrderId != 0)
                {
                    Database.instance.UpdateSalesOrder(salesorder);
                }
                else
                {
                    Database.instance.InsertSalesOrder(salesorder);
                }
                Console.WriteLine("Ændringerne blev gemt");
            }
            else
            {
                Console.WriteLine("Ingen ændringer");
            }


        }
    }
}
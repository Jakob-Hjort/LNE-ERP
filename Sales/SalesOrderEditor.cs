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
        SalesOrderHeader salesorder = new();

        public SalesOrderEditor(SalesOrderHeader salesOrder)
        {
            Title = "Redigerer for " + salesOrder.CustomerId;
            this.salesorder = salesOrder;
        }

        protected override void Draw()
        {
            ExitOnEscape();
            Form<SalesOrderHeader> form = new();

            form.TextBox("Ordrenummer", nameof(SalesOrderHeader.OrderNumber));
            form.TextBox("Oprettelsestidspunkt", nameof(SalesOrderHeader.Creationstime));
            form.TextBox("Gennemførelsestidspunkt", nameof(SalesOrderHeader.ImplementationTime));
            form.TextBox("CustomerID", nameof(SalesOrderHeader.CustomerId));
            form.TextBox("Tilstand", nameof(SalesOrderHeader.Status));

            if (form.Edit(salesorder))
            {
                if (salesorder.CustomerId != 0)
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
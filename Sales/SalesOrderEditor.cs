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
            form.SelectBox("Tilstand", nameof(SalesOrderHeader.Status));

            form.AddOption("Tilstand", "None", OrderStatus.None);
            form.AddOption("Tilstand", "Created", OrderStatus.Created);
            form.AddOption("Tilstand", "Confirmed", OrderStatus.Confirmed);
            form.AddOption("Tilstand", "Packed", OrderStatus.Packed);
            form.AddOption("Tilstand", "Done", OrderStatus.Done);

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
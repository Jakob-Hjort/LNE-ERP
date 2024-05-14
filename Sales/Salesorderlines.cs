using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderLines : Screen
    {
        public override string Title { get; set; } = "Sales Order Lines";
        SalesOrderHeader SalesOrder { get; set; }

        public SalesOrderLines(SalesOrderHeader salesOrder)
        {
            Title = "Order Lines for " + salesOrder.OrderNumber;
            this.SalesOrder = salesOrder;
        }

        protected override void Draw()
        {
            Console.WriteLine($"OrdreNummber: {SalesOrder.OrderNumber}");
            Console.WriteLine($"Oprettelsestidspunkt: {SalesOrder.Creationstime}");
            Console.WriteLine($"Gennemførelsestidspunkt: {SalesOrder.ImplementationTime}");
            Console.WriteLine($"CustomerID: {SalesOrder.CustomerId}");
            Console.WriteLine($"Tilstand: {SalesOrder.Status}");

            // Vis hver ordrelinje
            Console.WriteLine("Ordrelinjer:");
            foreach (var orderLine in SalesOrder.OrderLines)
            {
                Console.WriteLine($"Produkt: {orderLine.Vare}, Antal: {orderLine.Antal}");
            }

            Console.WriteLine("Press F3 to go back");
            AddKey(ConsoleKey.F3, () =>
            {
                // Gå tilbage til hovedsiden for salgsordre
                //Screen.GoBack();
            });
            ExitOnEscape();
        }



    }



}

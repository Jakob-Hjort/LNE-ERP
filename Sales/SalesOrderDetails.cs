using System;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderDetails : Screen
    {
        public override string Title { get; set; } = "Sales order";
        SalesOrderHeader SalesOrder = new();

        public SalesOrderDetails(SalesOrderHeader salesOrder)
        {
            Title = "Details for " + salesOrder.OrderNumber;
            this.SalesOrder = salesOrder;
        }

        protected override void Draw()
        {
            Console.WriteLine("OrdreNummber: {0}", SalesOrder.OrderNumber);
            Console.WriteLine("Gennemførelsestidspunkt: {0}", SalesOrder.ImplementationTime);
            Console.WriteLine("CustomerID: {0}", SalesOrder.CustomerId);
            Console.WriteLine("Tilstand: {0}", SalesOrder.Status);

            Console.WriteLine("OrderLines");
            foreach (var orderLine in SalesOrder.OrderLines) //Der kommer ikke nogle op Fordi OrderLines er NULL! Skal lave en Method ind i SalesDatabase.cs!
            {
                Console.WriteLine($"Produkt: {orderLine.Vare}, Antal: {orderLine.Antal}");
            }

            Console.WriteLine("Press F2 to edit");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new SalesOrderEditor(SalesOrder));
            });

            ExitOnEscape();
        }
    }
}

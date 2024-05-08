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
            Title = "Details for " + salesOrder.CustomerId;
            this.SalesOrder = salesOrder;
        }

        protected override void Draw()
        {
            SalesOrderHeader sales = new();
            Console.WriteLine("OrdreNummber: {0}", SalesOrder.OrderNumber);
            Console.WriteLine("Oprettelsestidspunkt: {0}", SalesOrder.Creationstime);
            Console.WriteLine("Gennemførelsestidspunkt: {0}", SalesOrder.ImplementationTime);
            Console.WriteLine("CustomerID: {0}", SalesOrder.CustomerId);
            Console.WriteLine("Tilstand: {0}", SalesOrder.Status);

            Console.WriteLine("Press F2 to edit");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new SalesOrderEditor(SalesOrder));
            });
            ExitOnEscape();
        }
    }
}

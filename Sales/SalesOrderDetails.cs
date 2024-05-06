using System;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderDetails : Screen
    {
        public override string Title { get; set; } = "Sales Order Details";
        private SalesOrder salesOrder;

        public SalesOrderDetails(SalesOrder salesOrder)
        {
            this.salesOrder = salesOrder;
            Title = "Details for Order " + salesOrder.SalesOrderId;
        }

        protected override void Draw()
        {

            Console.WriteLine(SalesOrder.SalesOrderId);
            Console.WriteLine("Customer ID: {0}",SalesOrderHeader.CustomerId);
            Console.WriteLine("{0} {1}", SalesOrder.FullName, SalesOrder.CustomerId);
            Console.WriteLine("Price: {0}", SalesOrder.Prices);

            Console.WriteLine("Press Enter to view Sales Order Header");
            AddKey(ConsoleKey.Enter, () =>
            {
                //Screen.Display(new SalesOrderHeader(salesOrder));
            });

            ExitOnEscape();
        }
    }
}

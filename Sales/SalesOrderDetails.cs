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
            Console.WriteLine($"Order Number: {salesOrder.SalesOrderId}");
            Console.WriteLine($"Date: {salesOrder.SalesOrderDate}");
            //Console.WriteLine($"Customer ID: {}");
            //Console.WriteLine($"Customer Name: {salesOrder.CustomerFirstName} {salesOrder.CustomerLastName}");
            //Console.WriteLine($"Amount: {salesOrder.Amount}");

            Console.WriteLine("Press Enter to view Sales Order Header");
            AddKey(ConsoleKey.Enter, () =>
            {
                //Screen.Display(new SalesOrderHeader(salesOrder));
            });

            ExitOnEscape();
        }
    }
}

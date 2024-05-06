using System;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderDetails : Screen
    {
        public override string Title { get; set; } = "Sales order";
        SalesOrder SalesOrder = new();

        public SalesOrderDetails(SalesOrder salesOrder)
        {
            Title = "Details for " + salesOrder.SalesOrderName;
            this.SalesOrder = salesOrder;
        }

        protected override void Draw()
        {
            Console.WriteLine("Sales Order Number: {0}", SalesOrder.SalesOrderId);
            Console.WriteLine("Date: {0}", SalesOrder.SalesOrderDate);
            Console.WriteLine("Customer ID: {0}", SalesOrder.CustomerId);
            Console.WriteLine("Customer: {0}", SalesOrder.FullName);
            Console.WriteLine("Amount: {0}", SalesOrder.Prices);

            Console.WriteLine("Press F2 to edit");
            AddKey(ConsoleKey.F2, () =>
            {
                
            });
            ExitOnEscape();
        }
    }
}

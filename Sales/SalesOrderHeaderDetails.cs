using System;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderHeaderDetails : Screen
    {
        public override string Title { get; set; } = "Sales order";
        SalesOrderHeader SalesOrder = new();

        public SalesOrderHeaderDetails(SalesOrderHeader salesOrder)
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
            Console.WriteLine(""); 

            ListPage<Orderline> listPagelines = new();

            listPagelines.AddKey(ConsoleKey.F1, createNewOrderLine);
            Console.WriteLine("Tryk F1 for at oprette en linje");

            listPagelines.AddKey(ConsoleKey.F2, editOrderLine);
            Console.WriteLine("Tryk F2 for at redigere en linje");

            listPagelines.AddKey(ConsoleKey.F3, deleteOrderLine);
            Console.WriteLine("Tryk F3 for at slette en linje");

            //add other keys!!

            listPagelines.AddColumn("Vare", nameof(Orderline.Vare));
            listPagelines.AddColumn("Antal", nameof (Orderline.Antal));
            listPagelines.AddColumn("Pris", nameof (Orderline.Pris));
            //add more colmns

            Console.WriteLine("");
            Console.WriteLine("---OrderLines---");
            Database.instance.LoadOrderLines(SalesOrder);
            foreach (var orderLine in SalesOrder.OrderLines)
            {
                listPagelines.Add(orderLine);
            }

            var orderlines = listPagelines.Select();
            if (orderlines != null)
            {
                Screen.Display(new SalesOrderLinesDetails(orderlines));
            }
            ExitOnEscape();
        }

        void createNewOrderLine(Orderline orderLine)
        {
            Orderline new_salesOrderLine = new();
            Screen.Display(new SalesOrderLinesEditor(new_salesOrderLine, SalesOrder.OrderNumber));
        }

        void editOrderLine(Orderline orderLine)
        {
            Screen.Display(new SalesOrderLinesEditor(orderLine, SalesOrder.OrderNumber));
        }

        void deleteOrderLine(Orderline orderLine)
        {
            Database.instance.DeleteSalesOrderLine(orderLine);
            Screen.Clear(this);
            Draw();
        }
    }
}

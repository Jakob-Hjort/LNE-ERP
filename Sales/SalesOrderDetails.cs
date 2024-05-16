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
            ListPage<Orderline> listline = new();

            SalesOrderHeader sales = new();
            Console.WriteLine("OrdreNummber: {0}", SalesOrder.OrderNumber);
            //Console.WriteLine("Oprettelsestidspunkt: {0}", SalesOrder.Creationstime);
            Console.WriteLine("Gennemførelsestidspunkt: {0}", SalesOrder.ImplementationTime);
            Console.WriteLine("CustomerID: {0}", SalesOrder.CustomerId);
            Console.WriteLine("Tilstand: {0}", SalesOrder.Status);

            listline.AddColumn("Varenavn", nameof(Orderline.Vare), 20);
            listline.AddColumn("Antal", nameof(Orderline.Antal), 5);
            listline.AddColumn("Pris", nameof(Orderline.Pris), 10);

            var salesOrderLines = SalesOrder.OrderLines;
            foreach (Orderline model in salesOrderLines)
            {
                listline.Add(model);
            }


            //var selectedSalesOrderLine = listline.Select();
            //if (selectedSalesOrderLine != null)
            //{
            //    Screen.Display(new SalesOrderDetails(selectedSalesOrderLine));
            //}


            Console.WriteLine("Press F2 to edit");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new SalesOrderEditor(SalesOrder));
            });
            ExitOnEscape();
        }
    }
}

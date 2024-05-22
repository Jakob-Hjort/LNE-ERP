using LNE_ERP.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderListPage : Screen
    {
        //Set the title of this page
        public override string Title { get; set; } = "Sales Orders";

        protected override void Draw()
        {
            ExitOnEscape();

            Console.CursorVisible = false;

            ListPage<SalesOrderHeader>listPage = new();

            listPage.AddKey(ConsoleKey.F1, createNewSalesOrder);
            Console.WriteLine("Tryk F1 for at oprette");

            listPage.AddKey(ConsoleKey.F2, editSalesOrder);
            Console.WriteLine("Tryk F2 for at redigere");

            listPage.AddKey(ConsoleKey.F3, ShowSalesOrderLines);
            Console.WriteLine("Tryk F3 for at vise listen af ordrer");

            listPage.AddKey(ConsoleKey.F5, removeSalesOrder);
            Console.WriteLine("Tryk F5 for at slette ");


            listPage.AddColumn("Ordre Nummer", nameof(SalesOrderHeader.OrderNumber), 12);
            listPage.AddColumn("Kundenummer", nameof(SalesOrderHeader.CustomerId), 11);
            //listPage.AddColumn("Oprettelse", nameof(SalesOrderHeader.Creationstime), 20);
            listPage.AddColumn("Produceret", nameof(SalesOrderHeader.ImplementationTime), 20);
            listPage.AddColumn("Tilstand", nameof(SalesOrderHeader.Status), 8);

            var salesOrders = Database.instance.GetSalesOrderHeader();
            foreach (SalesOrderHeader model in salesOrders)
            {
                listPage.Add(model);
            }

            var selectedSalesOrder = listPage.Select();
            if (selectedSalesOrder != null)
            {
                Screen.Display(new SalesOrderDetails(selectedSalesOrder));
            }
        }

        void createNewSalesOrder(SalesOrderHeader _)
        {
            SalesOrderHeader new_salesOrder = new();
            Screen.Display(new SalesOrderEditor(new_salesOrder));
        }

        void editSalesOrder(SalesOrderHeader salesOrder)
        {
            Screen.Display(new SalesOrderEditor(salesOrder));
        }

        void removeSalesOrder(SalesOrderHeader salesOrder)
        {
            Database.instance.DeleteSalesOrder(salesOrder);
            Screen.Clear(this);
            Draw();
        }

        void ShowSalesOrderLines(SalesOrderHeader salesOrder)
        {
            Screen.Display(new SalesOrderLinesDetails(salesOrder));
        }
        //void createNewSalesOrderLine(SalesOrderHeader _)
        //{
        //    SalesOrderHeader new_salesOrder = new();
        //    Screen.Display(new SalesOrderLinesEditor(new_salesOrder));
        //}
    }
}
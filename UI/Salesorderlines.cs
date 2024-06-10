using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class Salesorderlines : Screen
    {
        public override string Title { get; set; } = "Salgs ordrer linjer";

        SalesOrderHeader header;

        public Salesorderlines(SalesOrderHeader header)
        {
            this.header = header;
        }

        protected override void Draw()
        {
            ExitOnEscape();

            Console.CursorVisible = false;

            ListPage<Orderline> listPage = new();

            listPage.AddKey(ConsoleKey.F1, createNewSalesOrderLine);
            Console.WriteLine("Tryk F1 for at oprette");

            //listPage.AddKey(ConsoleKey.F2, editSalesOrderLine);
            Console.WriteLine("Tryk F2 for at redigere");

           // listPage.AddKey(ConsoleKey.F5, removeSalesOrderLine);
            Console.WriteLine("Tryk F5 for at slette ");


            listPage.AddColumn("Ordre Nummer", nameof(Orderline.Vare), 12);
            listPage.AddColumn("Kundenummer", nameof(Orderline.Antal), 11);
            listPage.AddColumn("Oprettelse", nameof(Orderline.Pris), 10);


            Database.instance.LoadOrderLines(header);
            foreach (Orderline model in header.OrderLines)
            {
                listPage.Add(model);
            }

            var selectedSalesOrderLine = listPage.Select();
            if (selectedSalesOrderLine != null)
            {
                //Screen.Display(new SalesOrderLineDetails(selectedSalesOrderLine));
            }
        }

        void createNewSalesOrderLine(Orderline _)
        {
            Console.WriteLine("VI er her");
            Orderline new_salesOrderLine = new();
            Screen.Display(new SalesOrderLinesEditor(new_salesOrderLine, header.OrderNumber));
        }
    }

}


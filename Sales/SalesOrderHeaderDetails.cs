﻿using System;
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
            Console.WriteLine(""); //Keeps this Empty for UI!

            ListPage<Orderline> listPagelines = new();

            listPagelines.AddKey(ConsoleKey.F1, createNewOrderLine);
            Console.WriteLine("Tryk F1 for at oprette produkt");

            listPagelines.AddKey(ConsoleKey.F2, editOrderLine);
            Console.WriteLine("Tryk F2 for at redigere produkt");

            listPagelines.AddKey(ConsoleKey.F3, deleteOrderLine);
            Console.WriteLine("Tryk F3 for at slette produkt");

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
                Console.WriteLine($"Produkt: {orderLine.Vare}, Antal: {orderLine.Antal}");
                listPagelines.Add(orderLine);
            }

            var orderlines = listPagelines.Select();
            if (orderlines != null)
            {
                Screen.Display(new SalesOrderLinesDetails(orderlines));
            }

            Console.WriteLine("Press F2 to edit");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new SalesOrderEditor(SalesOrder));
            });

            ExitOnEscape();
        }
        void createNewOrderLine(Orderline orderLine)
        {

        }
        void editOrderLine(Orderline orderLine)
        {

        }
        void deleteOrderLine(Orderline orderLine)
        {

        }
    }
}

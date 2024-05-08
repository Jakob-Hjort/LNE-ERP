﻿using System;
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

            listPage.AddKey(ConsoleKey.F5, removeSalesOrder);
            Console.WriteLine("Tryk F5 for at slette ");

            listPage.AddColumn("Salgs Ordre Nummer", nameof(SalesOrderHeader.OrderNumber), 15);
            listPage.AddColumn("Oprettelse", nameof(SalesOrderHeader.Creationstime), 15);
            //listPage.AddColumn("Kundenummer", nameof(Customer.CustomerNumber), 10);
            //listPage.AddColumn("Fornavn & Efternavn", nameof(Customer.Firstname) + nameof(Customer.Lastname), 30);
            //listPage.AddColumn("Beløb", nameof(SalesOrder.Prices), 20);

            var salesOrders = Database.instance.GetSalesOrderHeaders();
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
    }
}
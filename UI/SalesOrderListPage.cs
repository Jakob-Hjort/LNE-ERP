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

            //A ListPage draws columns and rows in a box
            ListPage<SalesOrder> listPage = new();

            //V4 - Add keys to create company
            listPage.AddKey(ConsoleKey.F1, createNewSalesOrder);
            Console.WriteLine("Press F1 to create sales order");

            //V5 - Add key to edit company
            listPage.AddKey(ConsoleKey.F2, editSalesOrder);
            Console.WriteLine("Press F2 to eidt sales order");

            //V6 - Add key to edit company
            listPage.AddKey(ConsoleKey.F5, removeSalesOrder);
            Console.WriteLine("Press F5 to delet sales order");

            //Add some columns
            listPage.AddColumn("SalesOrderName", nameof(SalesOrder.SalesOrderName), 30);
            listPage.AddColumn("Customer", nameof(SalesOrderHeader.CustomerId), 8);
            listPage.AddColumn("SalesOrderID", nameof(SalesOrder.SalesOrderId));
            listPage.AddColumn("Pris", nameof(SalesOrder.Prices), 8);
            

            //Get companies from the database and add them to the list
            var salesorders = Database.instance.GetSalesOrders();
            foreach (SalesOrder model in salesorders)
            {
                listPage.Add(model);
            }


            //Enable selection of a company by using arrow keys
            var salesOrder = listPage.Select();
            if (salesOrder != null)
            {
                //Ask Screen class to display the CompanyDetails view
                Screen.Display(new SalesOrderDetails(salesOrder));
            }
        }

        void createNewSalesOrder(SalesOrder _)
        {
            SalesOrder new_salesOrder = new();
            Screen.Display(new SalesOrderEditor(new_salesOrder));
        }

        void editSalesOrder(SalesOrder salesOrder)
        {
            Screen.Display(new SalesOrderEditor(salesOrder));
        }

        void removeSalesOrder(SalesOrder salesOrder)
        {
            Database.instance.DeleteSalesOrder(salesOrder);
            Screen.Clear(this);
            Draw();
        }
    }
}
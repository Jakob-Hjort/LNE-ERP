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

            //ListPage<SalesOrder> listPage = new();

            ListPage<SalesOrderHeader>  listPage = new();

            listPage.AddColumn("Salgs Ordre Nummer", nameof(SalesOrderHeader.OrderNumber), 15);
            listPage.AddColumn("Oprettelse", nameof(SalesOrderHeader.Creationstime), 15);
            listPage.AddColumn("Kundenummer", nameof(Customer.CustomerNumber), 10);
            listPage.AddColumn("Fornavn & Efternavn", nameof(Customer.Firstname) + nameof(Customer.Lastname), 30);
            listPage.AddColumn("Beløb", nameof(SalesOrder.Prices), 20);

            var salesOrders = Database.instance.GetSalesOrders();
            foreach (SalesOrder model in salesOrders)
            {
                listPage.Add(model);
            }

            var selectedSalesOrder = listPage.Select();
            if (selectedSalesOrder != null)
            {
                Screen.Display(new SalesOrderDetails(selectedSalesOrder));
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
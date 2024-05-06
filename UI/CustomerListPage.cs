using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class CustomerListPage : Screen
    {
        //Set the title of this page
        public override string Title { get; set; } = "Kunder";

        protected override void Draw()
        {
            ExitOnEscape();

            Console.CursorVisible = false;

            //A ListPage draws columns and rows in a box
            ListPage<Customer> listPage = new();

            //V4 - Add keys to create company
            listPage.AddKey(ConsoleKey.F1, createNewCustomer);
            Console.WriteLine("Tryk F1 for at oprette virksomhed");

            //V5 - Add key to edit company
            listPage.AddKey(ConsoleKey.F2, editCustomer);
            Console.WriteLine("Tryk F2 for at redigere virksomhed");

            //V6 - Add key to edit company
            listPage.AddKey(ConsoleKey.F5, removeCustomer);
            Console.WriteLine("Tryk F5 for at slette virksomhed");

            //Add some columns
            listPage.AddColumn("Kunde id", nameof(Customer.CustomerNumber), 4);
            listPage.AddColumn("Fulde navn", nameof(Customer.Fullname),10);
            listPage.AddColumn("Tlf. nummer", nameof(Customer.PhoneNumber), 8);
            listPage.AddColumn("Email", nameof(Customer.Email), 8);

            //Get companies from the database and add them to the list
            var customers = Database.instance.GetCustomer();
            foreach (Customer model in customers)
            {
                listPage.Add(model);
            }


            //Enable selection of a company by using arrow keys
            var customer = listPage.Select();
            if (customer != null)
            {
                //Ask Screen class to display the CompanyDetails view
                Screen.Display(new CustomerDetails(customer));
            }
        }

        void createNewCustomer(Customer _)
        {
            Customer new_customer = new();
            Screen.Display(new CustomerEditor(new_customer));
        }

        void editCustomer(Customer customer)
        {
            Screen.Display(new CustomerEditor(customer));
        }

        void removeCustomer(Customer customer)
        {
            Database.instance.DeleteCustomer(customer);
            Screen.Clear(this);
            Draw();
        }
    }
}

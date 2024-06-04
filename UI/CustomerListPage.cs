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
            Console.WriteLine("Tryk F1 for at oprette en kunde.");

            //V5 - Add key to edit company
            listPage.AddKey(ConsoleKey.F2, editCustomer);
            Console.WriteLine("Tryk F2 for at redigere kunde.");

            listPage.AddKey(ConsoleKey.F3, searchCustomer);
            Console.WriteLine("Tryk F3 for at søge kunde");

            //V6 - Add key to edit company
            listPage.AddKey(ConsoleKey.F5, removeCustomer);
            Console.WriteLine("Tryk F5 for at slette kunde.");

            //Add some columns
            listPage.AddColumn("Kunde id", nameof(Customer.PersonID), 10);
            listPage.AddColumn("Fulde navn", nameof(Customer.FullName),20);
            listPage.AddColumn("Tlf. nummer", nameof(Customer.PhoneNumber), 11);
            listPage.AddColumn("Email", nameof(Customer.Email), 30);

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

        void searchCustomer(Customer customer)
        {
            Screen.Clear();
            Console.Write("Input: ");
            string searchValue = Console.ReadLine();
            List<Customer> customers = Database.instance.SearchCustomer(searchValue);
            Console.WriteLine($"{customers.Count}");

            if (customers.Count == 0)
            {
                Console.WriteLine("\nNo match");
            }
            else if (customers.Count == 1)
            {
                Screen.Display(new CustomerEditor(customers.First()));
            }
            else
            {
                Console.WriteLine("Flere Kunder Fundet");
                foreach (var cust in customers)
                {
                   
                    Console.WriteLine($"\nCustomer: {cust.FirstName} {cust.LastName}");

                }
                Console.WriteLine("Tryk Enter For at kom Videre");
                Console.ReadLine();
                Screen.Clear();
            }
        }
        void createNewCustomer(Customer _)
        {
            Screen.Display(new CustomerEditor());
        }

        void editCustomer(Customer customer)
        {
            Screen.Display(new CustomerEditor(customer));
        }

        void removeCustomer(Customer customer)
        {
            try
            {
                Database.instance.DeleteCustomer(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Tryk på en tast for at fortsætte...");
                Console.ReadKey();
            }

            Screen.Clear(this);
            Draw();
        }
    }
}

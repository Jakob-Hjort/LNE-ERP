using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class CustomerDetails : Screen
    {

        public override string Title { get; set; } = "Kunder";
        Customer customer = new();

        public CustomerDetails(Customer customer)
        {
            Title = "Detaljer for " + customer.FullName;
            this.customer = customer;
        }

        protected override void Draw()
        {

                Console.WriteLine($"Kunde ID:{customer.CustomerNumber}");
                Console.WriteLine($"Fuldenavn:{customer.FullName}");
                Console.WriteLine($"-----------------------------");

            if (customer != null)
            {

                Console.WriteLine($"Vejnavn:{customer.Addresses.Streetname}");
                Console.WriteLine($"Hus nummer:{customer.Addresses.Housenumber}");
                Console.WriteLine($"Postnummer:{customer.Addresses.Postalcode}");
                Console.WriteLine($"By:{customer.Addresses.City}");

            }
            else
            {
                Console.WriteLine("Ingen Adresse tilgænlig");
            }
            Console.WriteLine($"-----------------------------");
            Console.WriteLine($"Sidste Købs dato:{customer.LastPurchaseDate}");
            //Console.WriteLine($"Telefon nummer:{customer.PhoneNumber}");
            //Console.WriteLine($"Email:{customer.Email}");
            Console.WriteLine($"");
            Console.WriteLine("Tryk F2 for at redigere");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new CustomerEditor(customer));
            });

            Console.WriteLine("Tryk F1 for at oprette en ny kunde");
            AddKey(ConsoleKey.F1, () =>
            {
                Screen.Display(new CustomerEditor());
            });
            Console.WriteLine("Tryk F3 for at slette denne kunde");
            AddKey(ConsoleKey.F3, () =>
            {

                Screen.Display(new CustomerEditor());
            });
            ExitOnEscape();
        }
    }

}


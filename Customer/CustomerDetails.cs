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
            Title = "Detaljer for " + customer.Fullname;
            this.customer = customer;
        }

        protected override void Draw()
        {

            Console.WriteLine($"Kunde ID:{customer.CustomerNumber}");
            Console.WriteLine($"Fuldenavn:{customer.Fullname}");
            Console.WriteLine($"Vejnavn:{customer.Streetname}");
            Console.WriteLine($"Hus nummer:{customer.Housenumber}");
            Console.WriteLine($"Postnummer:{customer.Postalcode}");
            Console.WriteLine($"By:{customer.City}");
            Console.WriteLine($"Telefon nummer:{customer.PhoneNumber}");
            Console.WriteLine($"Email:{customer.Email}");

            Console.WriteLine("Tryk F2 for at redigere");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new CustomerEditor(customer));
            });
            ExitOnEscape();
        }
    }

}


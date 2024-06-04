using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class CustomerEditor : Screen 
    {

        public override string Title { get; set; } = "Kunder"; // Properties
        Customer customer = new();


        public CustomerEditor(Customer customer) // Constructor
        {
            Title = "Redigerer for " + customer.FullName;
            this.customer = customer;
        }

        public CustomerEditor() // Constructor
        {
            Title = "Ny Kunde";
            this.customer = new Customer();
            this.customer.Addresses = new Addresses();
        }

        protected override void Draw() // Metode
        {
            ExitOnEscape();
            Form<Customer> form = new();

            form.TextBox("Fornavn", nameof(Customer.FirstName));
            form.TextBox("Efternavn", nameof(Customer.LastName));
            form.TextBox("Vejnavn", nameof(Customer.Street));
            form.TextBox("Husnummer", nameof(Customer.Housenumer));
            form.TextBox("Postnummer", nameof(Customer.postalcode));
            form.TextBox("By", nameof(Customer.city));
            form.TextBox("Tlf. nummer", nameof(Customer.PhoneNumber));
            form.TextBox("Email", nameof(Customer.Email));
            if (form.Edit(customer))
            {
                if (customer.CustomerID != 0)
                {
                    Database.instance.UpdateCustomer(customer);
                }
                else
                {
                    Database.instance.InsertCustomer(customer);
                }
                Console.WriteLine("Ændringerne blev gemt");
            }
            else
            {
                Console.WriteLine("Ingen ændringer");
            }


        }
    }
}


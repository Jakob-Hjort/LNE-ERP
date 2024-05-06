﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class CustomerEditor : Screen
    {

        public override string Title { get; set; } = "Kunder";
        Customer customer = new();

        public CustomerEditor(Customer customer)
        {
            Title = "Redigerer for " + customer.Fullname;
            this.customer = customer;
        }

        protected override void Draw()
        {
            ExitOnEscape();
            Form<Customer> form = new();

            //form.TextBox("Sales order ID", nameof(SalesOrder.SalesOrderId));
            form.TextBox("Fornavn", nameof(Customer.Firstname));
            form.TextBox("Efternavn", nameof(Customer.Lastname));
            form.TextBox("Vejnavn", nameof(Customer.Streetname));
            form.TextBox("Husnummer", nameof(Customer.Housenumber));
            form.TextBox("Postnummer", nameof(Customer.Postalcode));
            form.TextBox("Tlf. nummer", nameof(Customer.PhoneNumber));
            form.TextBox("Email", nameof(Customer.Email));
            if (form.Edit(customer))
            {
                if (customer.CustomerNumber != 0)
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

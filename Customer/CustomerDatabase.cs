﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        public void CustomerTestdata()
        {
           customerlist = new List<Customer>()

            {
            new Customer {Firstname = "Ejnars", Lastname = "codingleaf", Streetname = "Struervej", Housenumber = "70", Postalcode = 9220, City=  "Aalborg Ø", CustomerNumber = 1 ,LastPurchaseDate = new DateTime(2024, 4, 30), Email= "Thecodingleaf@actnow.dk", PhoneNumber = 88888888 },
            new Customer {Firstname = "Emil", Lastname = "Hytting", Streetname = "Struervej", Housenumber = "68", Postalcode = 9220, City=  "Aalborg Ø", CustomerNumber = 2 ,LastPurchaseDate = new DateTime(2024, 5, 23), Email= "Hytting@actnow.dk", PhoneNumber = 26262626}
            };
        }
        public Customer GetCustomerrById(int id)
        {
            foreach (var customer in customerlist)
            {
                if (customer.CustomerNumber == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public List<Customer> GetCustomer()
        {
            List<Customer> CustomerListCopy = new();
            CustomerListCopy.AddRange(customerlist);
            return CustomerListCopy;
        }

        public void InsertCustomer(Customer customer)
        {
            if (customer.CustomerNumber != 0)
            {
                return;
            }
            customer.CustomerNumber = customerlist.Count + 1;
            customerlist.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer.CustomerNumber == 0)
            {
                return;
            }

            for (var i = 0; i < customerlist.Count; i++)
            {
                if (customerlist[i].CustomerNumber == customer.CustomerNumber)
                {
                    customerlist[i] = customer;
                }
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            if (customer.CustomerNumber == 0)
            {
                return;
            }
            if (customerlist.Contains(customer))
            {
                customerlist.Remove(customer);
            }
        }
    }
}


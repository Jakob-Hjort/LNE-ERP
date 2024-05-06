using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Customer : Person
    {
        public int CustomerNumber { get; set; }
        public DateTime LastPurchaseDate { get; set; }


       /* public Customer(string firstname, string lastname, string address, string contactinfo, int customernumber, DateTime lastpurchasedate, string email, int phonenumber)
         : base(firstname, lastname, address, contactinfo, email, phonenumber)
        {
            CustomerNumber = customernumber;
            LastPurchaseDate = lastpurchasedate;

        }*/
    }
}


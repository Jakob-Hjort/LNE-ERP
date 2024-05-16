using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Customer : Person
    {

        public Addresses Addresses { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime LastPurchaseDate { get; set; }
        public string Street
        {
            get
            {
                return Addresses.Streetname;
            }
            set
            {
                Addresses.Streetname = value;
            }
        }

        public string Housenumer
        {
            get => Addresses.Housenumber;
            set => Addresses.Housenumber = value;
        }

        public int postalcode
        {
            get => Addresses.Postalcode;
            set => Addresses.Postalcode = value;
        }

        public string city
        {
            get => Addresses.City;
            set => Addresses.City = value;
        }
    }
}


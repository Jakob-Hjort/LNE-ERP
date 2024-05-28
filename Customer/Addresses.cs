using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Addresses
    {

        public int AddressID { get; set; }
        public required string Streetname { get; set; }

        public required string Housenumber { get; set; }

        public int Postalcode { get; set; }
        public required string City { get; set; }
    }
}

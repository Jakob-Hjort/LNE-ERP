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
        public string Streetname { get; set; } = string.Empty;

        public string Housenumber { get; set; } = string.Empty;

        public int Postalcode { get; set; }
        public string City { get; set; } = string.Empty;
    }
}

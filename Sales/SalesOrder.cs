using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class SalesOrder
    {
        public int salgsordrenummer { get; set; }

        public DateOnly dato {  get; set; }

        public string kundeNummer { get; set; }

        public string fuldeNavn { get; set; }

        public decimal beløb {  get; set; }


    }
}

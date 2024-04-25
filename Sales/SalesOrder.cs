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
        public int Salgsordrenummer { get; set; }

        public DateOnly dato {  get; set; }

        public string KundeNummer { get; set; }

        public string FuldeNavn { get; set; }

        public decimal Beløb {  get; set; }


    }
}

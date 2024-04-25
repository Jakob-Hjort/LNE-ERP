using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Products
    {
        public string int Varenummer { get; set; }

        public string Navn { get; set; }

        public string Beskrivelse { get; set; }

        public decimal Salgspris { get; set; }

        public decimal Indkøbspris { get; set; }

        public string Lokation { get; set; } // Lokation er et nummer på 4 tal/bogstaver (Hvad er bedst og bruge?)

        public decimal Antal {  get; set; }

        public string Enhed {  get; set; } // Enhed er en begrænset muligt f.eks. styk, timer  eller meter.


    }


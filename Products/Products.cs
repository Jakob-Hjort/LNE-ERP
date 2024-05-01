using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP.Products
{
    public class Products
    {
        public int ProductId { get; set; }
        public string Itemnumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Saleprice { get; set; }

        public decimal Purchaseprice { get; set; }

        public string Location { get; set; } // Lokation er et nummer på 4 tal/bogstaver (Hvad er bedst og bruge?)

        public decimal Quantity { get; set; }

        public ProductUnits Units { get; set; } // Enhed er en begrænset muligt f.eks. styk, timer  eller meter.


    }
}


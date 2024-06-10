using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Product

    {
        public int ProductId { get; set; }

        public string ?Name { get; set; }

        public string ?Description { get; set; }

        public decimal Saleprice { get; set; }

        public decimal Purchaseprice { get; set; }

        public string ?Location { get; set; } // Lokation er et nummer på 4 tal/bogstaver (Hvad er bedst og bruge?)

        public decimal Quantity { get; set; }

        public ProductUnits Units { get; set; } // Enhed er en begrænset muligt f.eks. styk, timer  eller meter

        public decimal AvanceiProcent
        {
            get
            {
                if (Saleprice == 0)
                {
                    return 0;
                }
                else
                {
                    decimal avance = 100 * (this.Saleprice / this.Purchaseprice);
                    return Math.Round(avance, 2); // Begræns til to decimaler
                }
            }
        }
        public decimal AvanceiKr
        {
            get
            {
                if (Saleprice == 0)
                { return 0; }
                else return (this.Saleprice - this.Purchaseprice);
            }
        }
    }
}


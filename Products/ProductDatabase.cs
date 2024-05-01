using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP.Products
{
    public partial class Database
    {

        // Beregne Fortjeneste.
        public decimal CalculateProfit(Products product)
        {
            return product.P_Salgspris - product.P_Indkøbspris;
        }

        // Beregne avance i procent.
        public decimal CalculateMarginPercentage(Products produkt)
        {
            if (produkt.P_Indkøbspris == 0)
                return 0; 
            else
                return (CalculateProfit(produkt) / produkt.P_Indkøbspris) * 100;
        }
    }
}


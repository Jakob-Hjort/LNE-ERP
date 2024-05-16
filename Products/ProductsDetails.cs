using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    internal class ProductsDetails : Screen
    {
        public override string Title { get; set; } = "Product";
        Product Product = new();

        public ProductsDetails(Product product)
        {
            Title = "Detaljer for " + product.Name;
            this.Product = product;
        }
        protected override void Draw()
        {
            Console.WriteLine($"VareNummer:{Product.ProductId}");
            Console.WriteLine($"Navn:{Product.Name}");
            Console.WriteLine($"Beskrivelse:{Product.Description}");
            Console.WriteLine($"-----------------------------");
            Console.WriteLine($"Salgspris:{Product.Saleprice}");
            Console.WriteLine($"Indkøbspris:{Product.Purchaseprice}");
            Console.WriteLine($"-----------------------------");
            Console.WriteLine($"Lokation:{Product.Location}");
            Console.WriteLine($"Antal på Lager:{Product.Quantity}");
            Console.WriteLine($"Enhed:{Product.Units}");
            Console.WriteLine($"-----------------------------");
            Console.WriteLine($"Avance i Procent:{Product.AvanceiProcent}");
            Console.WriteLine($"Avance i Kr:{Product.AvanceiKr}");
            Console.WriteLine($"-----------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Tryk F2 for at redigere");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new ProductEditor(Product));
            });
            ExitOnEscape();
        }
    }
}

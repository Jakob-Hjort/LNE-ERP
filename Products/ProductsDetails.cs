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
            Console.WriteLine($"ID:{Product.ProductId}");
            Console.WriteLine($"ID:{Product.Name}");
            Console.WriteLine($"ID:{Product.Itemnumber}");
            Console.WriteLine($"ID:{Product.Saleprice}");
            Console.WriteLine($"ID:{Product.Quantity}");

            Console.WriteLine("Tryk F2 for at redigere");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new ProductEditor(Product));
            });
            ExitOnEscape();
        }
    }
}

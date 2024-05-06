using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    internal class ProductEditor : Screen
    {
        public override string Title { get; set; } = "Produkt";
        Product Products = new();

        public ProductEditor(Product product)
        {
            Title = "Redigerer for " + product.Name;
            this.Products = product;
        }

        protected override void Draw()
        {
            ExitOnEscape();
            Form<Product> form = new();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-Du SKAL gem ændringer efter hver linje-");
            Console.ResetColor();

            form.TextBox("VareNummer", nameof(Product.Itemnumber));
            form.TextBox("Navn", nameof(Product.Name));
            form.TextBox("Beskrivelse", nameof(Product.Description));
            form.TextBox("Salgspris", nameof(Product.Saleprice));
            form.TextBox("Indkøbsspris", nameof(Product.Purchaseprice));
            form.TextBox("Lokation", nameof(Product.Location));
            form.TextBox("Antal på lager", nameof(Product.Quantity));
            form.TextBox("Enhed", nameof(Product.Units));

            //form.AddOption("Currency", "DKK", Currency.DKK);
            //form.AddOption("Currency", "EUR", Currency.EUR);
            //form.AddOption("Currency", "USD", Currency.USD);
            //form.AddOption("Currency", "SEK", Currency.SEK);

            if (form.Edit(Products))
            {
                if (Products.ProductId != 0)
                {
                    Database.instance.UpdateProduct(Products);
                }
                else
                {
                    Database.instance.InsertProduct(Products);
                }
                Console.WriteLine("A Ændringerne blev gemt");
            }
            else
            {
                Console.WriteLine("Ingen ændringer");
            }


        }
    }
}

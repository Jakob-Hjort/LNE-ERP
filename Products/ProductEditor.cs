﻿using System;
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

            //form.TextBox("VareNummer", nameof(Product.ProductId));
            Console.WriteLine(" ");
            Console.WriteLine("             VareNummer - " + Products.ProductId);
            Console.WriteLine(" ");
            form.TextBox("Navn", nameof(Product.Name));
            form.TextBox("Beskrivelse", nameof(Product.Description));
            form.TextBox("Salgspris", nameof(Product.Saleprice));
            form.TextBox("Indkøbsspris", nameof(Product.Purchaseprice));
            form.TextBox("Lokation", nameof(Product.Location));
            form.TextBox("Antal på lager", nameof(Product.Quantity));
            form.SelectBox("Enhed", nameof(Product.Units));

            form.AddOption("Enhed", "Stk", ProductUnits.stk);
            form.AddOption("Enhed", "Meter", ProductUnits.meter);
            form.AddOption("Enhed", "Timer", ProductUnits.timer);

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

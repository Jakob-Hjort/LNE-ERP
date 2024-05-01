using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP.Products
{
    public partial class Database
    {
        private List<Products> products = new List<Products>();

         
        public Products GetProductById(int id) //Hent produkt ud fra ID
        {
            return products.FirstOrDefault(p => p.ProductId == id);
        }

        public Database(List<Products> existingProducts) // Konstruktør der modtager en liste af produkter
        {
            products = existingProducts;
        }

        public List<Products> GetAllProducts() // Hent alle produkter og retuner en liste
        {
            return products.ToList();   
        }

        
        public void InsertProduct(Products product) // Indsæt produkter: Lægger produkter i databasen
        {
            products.Add(product);
        }

        public void UpdateProduct(Products product, int id) // Opdater et produkt i databasen
        {
            Products existingProduct = GetProductById(id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Saleprice = product.Saleprice;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Unit = product.Unit;
                existingProduct.Location = product.Location;
                existingProduct.Itemnumber = product.Itemnumber;
            }
        }

        public void DeleteProductById(int id) // Slet produkt ud fra id: Metode der tager et id som parameter og fjerner det fra databasen
        {
            Products productToRemove = GetProductById(id);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
            }
        }

        public decimal CalculateProfit(Products product) // Beregne Fortjeneste.
        {
            return product.Saleprice - product.Purchaseprice;
        }

        public decimal CalculateMarginPercentage(Products product) // Beregne avance i procent.
        {
            if (product.Saleprice == 0)
                return 0; 
            else
                return (CalculateProfit(product) / product.Saleprice) * 100;
        }
    }
}


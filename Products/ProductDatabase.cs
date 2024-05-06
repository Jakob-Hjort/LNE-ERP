using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        List<Product> products = new();

        public Product GetProductById(int id)
        {
            foreach (var product in products)
            {
                if (product.ProductId == id)
                {
                    return product;
                }
            }
            return null;
        }
        public List<Product> GetProducts()
        {
            List<Product> ProductCopy = new();
            ProductCopy.AddRange(products);
            return ProductCopy;
        }

        public void InsertProduct(Product product)
        {
            if (product.ProductId != 0)
            {
                return;
            }
            product.ProductId = products.Count + 1;
            products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                return;
            }

            for (var i = 0; i < products.Count; i++)
            {
                if (products[i].ProductId == product.ProductId)
                {
                    products[i] = product;
                }
            }
        }

        public void DeleteProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                return;
            }
            if (products.Contains(product))
            {
                products.Remove(product);
            }
        }
    }
}


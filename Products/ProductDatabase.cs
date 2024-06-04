using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        List<Product> products = new();

        public Product? GetProductById(int id)
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
            List<Product> productList = new();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string sql = "Select ProductId, ProductName, ProductDescription, ProductQuantity, ProductUnits, ProductSalesPrice, ProductPurchasePrice FROM Products";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new()
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Quantity = reader.GetInt32(3),
                            Units = (ProductUnits)reader.GetInt32(4),
                            Saleprice = reader.GetDecimal(5),
                            Purchaseprice = reader.GetDecimal(6)
                        };

                        productList.Add(product);
                    }
                }
            }

            return productList;
        }

        public void InsertProduct(Product product)
        {
            if (product.ProductId != 0)
            {
                return;
            }

            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Products (ProductName, ProductDescription, ProductQuantity, ProductUnits, ProductSalesPrice, ProductPurchasePrice) VALUES (@ProductName, @ProductDescription, @ProductQuantity, @ProductUnits, @ProductSalesPrice, @ProductPurchasePrice)";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@ProductName", product.Name);
                command.Parameters.AddWithValue("@ProductDescription", product.Description);
                command.Parameters.AddWithValue("@ProductQuantity", product.Quantity);
                command.Parameters.AddWithValue("@ProductUnits", product.Units);
                command.Parameters.AddWithValue("@ProductSalesPrice", product.Saleprice);
                command.Parameters.AddWithValue("@ProductPurchasePrice", product.Purchaseprice);
 
                try
                {
                    command.ExecuteNonQuery();
                    //get id
                    command = conn.CreateCommand();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    product.ProductId = reader.GetInt32(0);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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

            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "UPDATE Products SET ProductName = @ProductName, ProductDescription = @ProductDescription, ProductQuantity = @ProductQuantity, ProductUnits = @ProductUnits, ProductSalesPrice = @ProductSalesPrice, ProductPurchasePrice = @ProductPurchasePrice WHERE ProductId = @ProductId";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@ProductName", product.Name);
                command.Parameters.AddWithValue("@ProductDescription", product.Description);
                command.Parameters.AddWithValue("@ProductQuantity", product.Quantity);
                command.Parameters.AddWithValue("@ProductUnits", product.Units);
                command.Parameters.AddWithValue("@ProductSalesPrice", product.Saleprice);
                command.Parameters.AddWithValue("@ProductPurchasePrice", product.Purchaseprice);

                // Opdater Produktet i databasen
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Opdater Produktet i listen, hvis den blev opdateret i databasen
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].ProductId == product.ProductId)
                        {
                            products[i] = product;
                            break; // Vi har fundet og opdateret virksomheden, så vi kan afslutte løkken
                        }
                    }
                }
            }
        }

        public void DeleteProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                return;
            }

            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Products WHERE ProductId = @ProductId";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@ProductId", product.ProductId);

                // Fjern virksomheden fra databasen
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Fjern virksomheden fra listen, hvis den blev slettet fra databasen
                    if (products.Contains(product))
                    {
                        products.Remove(product);
                    }
                }
            }
        }
    }
}


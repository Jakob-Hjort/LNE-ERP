using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        List<SalesOrderHeader> Sales = new();

        public SalesOrderHeader GetSalesOrderById(int id)
        {
            foreach (var salesorder in Sales)
            {
                if (salesorder.OrderNumber == id)
                {
                    return salesorder;
                }
            }
            return null;
        }


        // --------------  SALES ORDER HEADER -------------------------//


        public List<SalesOrderHeader> GetSalesOrderHeader()
        {
            List<SalesOrderHeader> sales = new();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string sql = "Select OrderNumber, ImplementationTime, CustomerId, Status FROM SalesOrderHeader";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesOrderHeader header = new();
                        header.OrderNumber = reader.GetInt32(0);
                        //header.Creationstime = reader.GetDateTime(1);
                        header.ImplementationTime = reader.GetDateTime(1);
                        header.CustomerId = reader.GetInt32(2);
                        header.Status = (OrderStatus)reader.GetInt32(3);
                        sales.Add(header);
                    }
                }
            }
            foreach (var s in sales)
            {
                LoadOrderLines(s);
            }
            return sales;
        }

        public void UpdateSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber == 0)
            {
                return;
            }
            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "UPDATE SalesOrderHeader SET ImplementationTime = @ImplementationTime, CustomerId = @CustomerId, Status = @Status WHERE OrderNumber = @OrderNumber";
                SqlCommand command = new SqlCommand(sql, conn);
                //command.Parameters.AddWithValue("@Creationtime", salesorder.Creationstime);
                command.Parameters.AddWithValue("@ImplementationTime", salesorder.ImplementationTime);
                command.Parameters.AddWithValue("@CustomerId", salesorder.CustomerId);
                command.Parameters.AddWithValue("@Status", salesorder.Status);
                command.Parameters.AddWithValue("@OrderNumber", salesorder.OrderNumber);


                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    for (int i = 0; i < Sales.Count; i++)
                    {
                        if (Sales[i].OrderNumber == salesorder.OrderNumber)
                        {
                            Sales[i] = salesorder;
                            break;
                        }
                    }
                }
            }
        }

        public void InsertSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber != 0)
            {
                return;
            }
            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "INSERT INTO SalesOrderHeader (ImplementationTime, CustomerId, Status) VALUES (@ImplementationTime, @CustomerId, @Status)";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@ImplementationTime", salesorder.ImplementationTime);
                command.Parameters.AddWithValue("@CustomerId", salesorder.CustomerId);
                command.Parameters.AddWithValue("@Status", salesorder.Status);
                try
                {
                    command.ExecuteNonQuery();
                    command = conn.CreateCommand();
                    command.CommandText = "SELECT SCOPE_IDENTITY();";
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    salesorder.OrderNumber = reader.GetInt32(0);
                    //InserSalesOrderList(salesorder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                conn.Close();

            }
            salesorder.OrderNumber = Sales.Count + 1;
            Sales.Add(salesorder);
        }


        public void DeleteSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber == 0)
            {
                return;
            }
            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "DELETE FROM OrderLines WHERE OrderNumber = @OrderNumber; DELETE FROM SalesOrderHeader WHERE OrderNumber = @OrderNumber";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@OrderNumber", salesorder.OrderNumber);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    if (Sales.Contains(salesorder))
                    {
                        Sales.Remove(salesorder);
                    }
                }
            }
        }


        // -------------------- SALES ORDER LINES ----------------------------- //

        public void LoadOrderLines(SalesOrderHeader header)
        {
            header.OrderLines = new();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string sql = "SELECT Vare, Pris, Antal FROM OrderLines WHERE OrderNumber = " + header.OrderNumber; 
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@OrderNumber", header.OrderNumber);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Orderline line = new();
                        line.Vare = reader.GetString(0);
                        line.Pris = reader.GetDecimal(1);
                        line.Antal = reader.GetInt32(2);
                        header.OrderLines.Add(line);
                    }
                }
            }
        }
        
        public void InsertSalesOrderList(SalesOrderHeader salesorder)
        {
            salesorder.OrderLines = new();
            if (salesorder.OrderNumber != 0)
            {
                return;
            }
            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "INSERT INTO SalesOrderHeader (Vare, Pris, Antal) VALUES (@Vare, @Pris, @Antal)";
                SqlCommand command = new SqlCommand(@sql, conn);
                Orderline line = new();
                command.Parameters.AddWithValue("@vare", line.Vare);
                command.Parameters.AddWithValue("@Pris", line.Pris);
                command.Parameters.AddWithValue("@Antal", line.Antal);
                try
                {
                    command.ExecuteNonQuery();
                    command = conn.CreateCommand();
                    command.CommandText = "SELECT SCOPE_IDENTITY();";
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    salesorder.OrderNumber = reader.GetInt32(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                conn.Open();
            }
        }
        

        public void UpdateSalesOrderLines(Orderline orderline) 
        {
            if (orderline.OrderLineID == 0)
            {
                return;
            }
            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "UPDATE SalesOrderHeader SET ImplementationTime = @ImplementationTime, CustomerId = @CustomerId, Status = @Status WHERE OrderNumber = @OrderNumber";
                SqlCommand command = new SqlCommand(sql, conn);
                //command.Parameters.AddWithValue("@Creationtime", salesorder.Creationstime);
                command.Parameters.AddWithValue("@ImplementationTime", salesorder.ImplementationTime);
                command.Parameters.AddWithValue("@CustomerId", salesorder.CustomerId);
                command.Parameters.AddWithValue("@Status", salesorder.Status);
                command.Parameters.AddWithValue("@OrderNumber", salesorder.OrderNumber);


                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    for (int i = 0; i < Sales.Count; i++)
                    {
                        if (Sales[i].OrderNumber == salesorder.OrderNumber)
                        {
                            Sales[i] = salesorder;
                            break;
                        }
                    }
                }
            }
        }

        
    }
}
    


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
        public List<SalesOrderHeader> GetSalesOrderHeader()
        {
            List<SalesOrderHeader> sales = new();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string sql = "Select OrderNumber, Creationtime, ImplementationTime, CustomerId, Status FROM SalesOrderHeader";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesOrderHeader header = new();
                        header.OrderNumber = reader.GetInt32(0);
                        header.Creationstime = reader.GetDateTime(1);
                        header.ImplementationTime = reader.GetDateTime(2);
                        header.CustomerId = reader.GetInt32(3);
                        header.Status = (OrderStatus)reader.GetInt32(4);
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
        public void LoadOrderLines(SalesOrderHeader header)
        {
            header.OrderLines = new();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string sql = "Select Vare, Pris FROM Orderlines WHERE salesOrder = " + header.OrderNumber;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

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

        public void InsertSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber != 0)
            {
                return;
            }
            
            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Sales (";
            }
        }



        public void UpdateSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber == 0)
            {
                return;
            }

            for (var i = 0; i < salesOrderHeader.Count; i++)
            {
                if (salesOrderHeader[i].OrderNumber == salesorder.OrderNumber)
                {
                    salesOrderHeader[i] = salesorder;
                }
            }
        }

        public void DeleteSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber == 0)
            {
                return;
            }
            if (salesOrderHeader.Contains(salesorder))
            {
                salesOrderHeader.Remove(salesorder);
            }
        }

        //
        //  ------------ SALES ORDER LINES -------------
        /*

        public void TESTDATAORDERLINES()
        {
            salesOrderLines = new List<Orderline>()
            {
                new Orderline() { Vare = "Første licens til ERN systemet",Antal= 10,Pris=1000}
            };
        }


        public List<Orderline> GetSalesOrderLine()
        {
            List<Orderline> Ordercopy = new();
            Ordercopy.AddRange(salesOrderLine);
            return Ordercopy;
        }

        public void InsertSalesOrderLines(Orderline salesorderline)
        {
            if (salesorderline. != 0)
            {
                return;
            }
            salesorder.OrderNumber = salesOrderHeader.Count + 1;
            salesOrderHeader.Add(salesorder);
        }

        public void UpdateSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber == 0)
            {
                return;
            }

            for (var i = 0; i < salesOrderHeader.Count; i++)
            {
                if (salesOrderHeader[i].OrderNumber == salesorder.OrderNumber)
                {
                    salesOrderHeader[i] = salesorder;
                }
            }
        }

        public void DeleteSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber == 0)
            {
                return;
            }
            if (salesOrderHeader.Contains(salesorder))
            {
                salesOrderHeader.Remove(salesorder);
            }
        }*/

        //public List<Orderline> GetSalesOrderLines()
        //{
        //    List<Orderline> orderLinesCopy = new List<Orderline>();

        //    // Loop through each sales order header
        //    foreach (var salesOrder in salesOrderHeader)
        //    {
        //        // Loop through each order line in the sales order header
        //        foreach (var orderLine in salesOrder.OrderLines)
        //        {
        //            // Add the order line to the copy list
        //            orderLinesCopy.Add(orderLine);
        //        }
        //    }

        //    return orderLinesCopy;
        //}
    }
}
    


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        public void Testdata()
        {
            salesOrderHeader = new List<SalesOrderHeader>()
            {
                new SalesOrderHeader() { OrderNumber = 1109}
            };
            //salesorderlist = new List<SalesOrder>()
            //{
            //new SalesOrder {SalesOrderId = 1, SalesOrderName = "First Sales Order", SalesOrderDate = new DateOnly(2024, 4, 30), Prices = 30000, CustomerId = "1" },
            //new SalesOrder {SalesOrderId = 2, SalesOrderName = "Second Sales Order", SalesOrderDate = new DateOnly(2024, 5, 1), Prices = 60000, CustomerId = "2" }
            //};
        }


        public SalesOrderHeader GetSalesOrderById1(int id)
        {
            foreach (var salesorder in salesOrderHeader)
            {
                if (salesorder.CustomerId == id)
                {
                    return salesorder;
                }
            }
            return null;
        }

        public List<SalesOrderHeader> GetSalesOrders()
        {
            List<SalesOrderHeader> salesOrderCopy = new();
            salesOrderCopy.AddRange(salesOrderHeader);
            return salesOrderCopy;
        }
        public List<SalesOrderHeader> GetSalesOrderHeaders()
        {
            List<SalesOrderHeader> salesOrderHeaders = new();
            salesOrderHeader.AddRange(salesOrderHeaders);
            return salesOrderHeaders;
        }

        public void InsertSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.CustomerId != 0)
            {
                return;
            }
            salesorder.CustomerId = salesOrderHeader.Count+1;
            salesOrderHeader.Add(salesorder);
        }

        public void UpdateSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.CustomerId == 0)
            {
                return;
            }

            for (var i = 0; i < salesOrderHeader.Count; i++)
            {
                if (salesOrderHeader[i].CustomerId == salesorder.CustomerId)
                {
                    salesOrderHeader[i] = salesorder;
                }
            }
        }

        public void DeleteSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.CustomerId == 0)
            {
                return;
            }
            if (salesOrderHeader.Contains(salesorder))
            {
                salesOrderHeader.Remove(salesorder);
            }
        }
    }
}
    


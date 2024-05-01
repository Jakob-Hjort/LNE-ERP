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
       salesorderlist = new List<SalesOrder>()
            {
            new SalesOrder {SalesOrderId = 1, SalesOrderName = "First Sales Order", SalesOrderDate = new DateOnly(2024, 4, 30), Prices = 30000, CustomerId = "1" },
            new SalesOrder {SalesOrderId = 2, SalesOrderName = "Second Sales Order", SalesOrderDate = new DateOnly(2024, 5, 1), Prices = 60000, CustomerId = "2" }
        };

        }
        public SalesOrder GetSalesOrderById1(int id)
        {
            foreach (var salesorder in salesorderlist)
            {
                if (salesorder.SalesOrderId == id)
                {
                    return salesorder;
                }
            }
            return null;
        }

        public List<SalesOrder> GetSalesOrders()
        {
            List<SalesOrder> salesOrderCopy = new();
            salesOrderCopy.AddRange(salesorderlist);
            return salesOrderCopy;
        }

        public void InsertSalesOrder(SalesOrder salesorder)
        {
            if (salesorder.SalesOrderId != 0)
            {
                return;
            }
            salesorder.SalesOrderId = salesorderlist.Count+1;
            salesorderlist.Add(salesorder);
        }

        public void UpdateSalesOrder(SalesOrder salesorder)
        {
            if (salesorder.SalesOrderId == 0)
            {
                return;
            }

            for (var i = 0; i < salesorderlist.Count; i++)
            {
                if (salesorderlist[i].SalesOrderId == salesorder.SalesOrderId)
                {
                    salesorderlist[i] = salesorder;
                }
            }
        }

        public void DeleteSalesOrder(SalesOrder salesorder)
        {
            if (salesorder.SalesOrderId == 0)
            {
                return;
            }
            if (salesorderlist.Contains(salesorder))
            {
                salesorderlist.Remove(salesorder);
            }
        }
    }
}
    


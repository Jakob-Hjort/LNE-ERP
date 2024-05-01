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

        List<SalesOrder> salesorderslist = new() {
            new SalesOrder {SalesOrderId = 1, SalesOrderName = "First Sales Order", SalesOrderDate = new DateOnly(2024, 4, 30), Prices = 30000 },
            new SalesOrder {SalesOrderId = 2, SalesOrderName = "Second Sales Order", SalesOrderDate = new DateOnly(2024, 5, 1), Prices = 60000 }
        };
        public SalesOrder GetSalesOrderById1(int id)
        {
            foreach (var salesorder in salesorderliste)
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
            salesOrderCopy.AddRange(salesorderliste);
            return salesOrderCopy;
        }

        public void InsertSalesOrder(SalesOrder salesorder)
        {
            if (salesorder.SalesOrderId != 0)
            {
                return;
            }
            salesorder.SalesOrderId = salesorderliste.Count;
            salesorderliste.Add(salesorder);
        }

        public void UpdateSalesOrder(SalesOrder salesorder)
        {
            if (salesorder.SalesOrderId == 0)
            {
                return;
            }

            for (var i = 0; i < salesorderliste.Count; i++)
            {
                if (salesorderliste[i].SalesOrderId == salesorder.SalesOrderId)
                {
                    salesorderliste[i] = salesorder;
                }
            }
        }

        public void DeleteSalesOrder(SalesOrder salesorder)
        {
            if (salesorder.SalesOrderId == 0)
            {
                return;
            }
            if (salesorderliste.Contains(salesorder))
            {
                salesorderliste.Remove(salesorder);
            }
        }
    }
}
    


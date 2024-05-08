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
        }

        //List<SalesOrderHeader> salesOrderHeader = new();

        public SalesOrderHeader GetSalesOrderById1(int id)
        {
            foreach (var salesorder in salesOrderHeader)
            {
                if (salesorder.OrderNumber == id)
                {
                    return salesorder;
                }
            }
            return null;
        }
        public List<SalesOrderHeader> GetSalesOrderHeaders()
        {
            List<SalesOrderHeader> Ordercopy = new();
            Ordercopy.AddRange(salesOrderHeader);
            return Ordercopy;
        }

        public void InsertSalesOrder(SalesOrderHeader salesorder)
        {
            if (salesorder.OrderNumber != 0)
            {
                return;
            }
            salesorder.OrderNumber = salesOrderHeader.Count+1;
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
        }
    }
}
    


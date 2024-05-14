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
            Orderline Orderline1 = new Orderline { Vare = "Første licens til ERN systemet", Antal = 10, Pris = 1000 };
            Orderline Orderline2 = new Orderline { Vare = "Anden licens til ERN systemet", Antal = 5, Pris = 5000 };
            Orderline Orderline3 = new Orderline { Vare = "Tredje licens til ERN systemet", Antal = 2, Pris = 111000 };


            salesOrderHeader = new List<SalesOrderHeader>()
            {
                new SalesOrderHeader() {OrderNumber = 1109,
                OrderLines = new List<Orderline>() {Orderline1, Orderline2, Orderline3}}
            };
        }

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

        public List<Orderline> GetSalesOrderLines()
        {
            List<Orderline> orderLinesCopy = new List<Orderline>();

            // Loop through each sales order header
            foreach (var salesOrder in salesOrderHeader)
            {
                // Loop through each order line in the sales order header
                foreach (var orderLine in salesOrder.OrderLines)
                {
                    // Add the order line to the copy list
                    orderLinesCopy.Add(orderLine);
                }
            }

            return orderLinesCopy;
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
    }
}
    


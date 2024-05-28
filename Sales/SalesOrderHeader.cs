﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class SalesOrderHeader
    {
        public int OrderNumber { get; set; }
        public DateOnly Creationstime { get; set; }
        public DateTime ImplementationTime { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public List<Orderline> OrderLines { get; set; }
        public decimal OrderAmount => OrderLines.Sum(ol => ol.Pris * ol.Antal);
        public SalesOrderHeader()
        {
            OrderLines = new List<Orderline>();
        }
        //public SalesOrderHeader(int ordernumber, DateOnly creationtime, DateTime implementationtime,
        //    int customerid, OrderStatus status, List<Orderline> ordrelines)
        //{
        //    OrderNumber = ordernumber;
        //    Creationstime = creationtime;
        //    ImplementationTime = implementationtime;
        //    CustomerId = customerid;
        //    Status = status;
        //    OrderLines = ordrelines;
        //}
    }
    public class Orderline
    {
        public int OrderLineID { get; set; }
        public string Vare { get; set; } = string.Empty;
        public decimal Pris { get; set; }
        public int Antal { get; set; }
        public Orderline() {}

        //public Orderline(string vare, decimal pris, int antal)
        //{
        //    Vare = vare;
        //    Pris = pris;
        //    Antal = antal;
        //}
    }

}

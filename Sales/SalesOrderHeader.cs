using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class SalesOrderHeader
    {
        public int OrderNumber { get; set; }
        public DateTime Creationstime { get; set; }
        public DateTime ImplementationTime { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public List<Orderline> OrdreLines { get; set; }
        public decimal OrderAmount => OrdreLines.Sum(ol => ol.Pris * ol.Antal);
        public SalesOrderHeader() { }
        public SalesOrderHeader(int ordernumber, DateTime creationtime, DateTime implementationtime,
            int customerid, OrderStatus status, List<Orderline> ordrelines)
        {
            OrderNumber = ordernumber;
            Creationstime = creationtime;
            ImplementationTime = implementationtime;
            CustomerId = customerid;
            Status = status;
            OrdreLines = ordrelines;
        }

       /* public int Customertest
        {
            get
            {
                return customer.CustomNumber;
            }
            set
            {
                customer = value;
            }
        }*/
    }

    public class Orderline
    {
        public string Vare { get; set; }
        public decimal Pris { get; set; }
        public int Antal { get; set; }

        public Orderline(string vare, decimal pris, int antal)
        {
            Vare = vare;
            Pris = pris;
            Antal = antal;
        }
    }

}

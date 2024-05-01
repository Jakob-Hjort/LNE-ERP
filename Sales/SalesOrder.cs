using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class SalesOrder
    {
        public int SalesOrderId { get; set; }

        public string SalesOrderName { get; set; }

        public DateOnly SalesOrderDate {  get; set; }

        public string CustomerID { get; set; }

        public string FullName { get; set; }

        public decimal Prices {  get; set; }

        public Person Customer { get; set; }


    }
}

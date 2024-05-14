using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        public static Database instance { get; set; } = new Database();
        List<Company> companyList = new List<Company>();
        List<Customer> customerlist = new List<Customer>();
        List<SalesOrderHeader> salesOrderHeader = new List<SalesOrderHeader>();
        List<Product> productlist = new List<Product>();
        List<Orderline> salesorderlines = new List<Orderline>();
   


    }

        
}


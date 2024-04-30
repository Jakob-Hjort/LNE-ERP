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
        public SalesOrder GetSalesOrderById(int id)
        {
            foreach (SalesOrder s in salesorderliste)
            {
                {
                    if (s.Id == id)
                    {
                        return s;
                    }
                }
               
            }
            return null;
        }
    }
}

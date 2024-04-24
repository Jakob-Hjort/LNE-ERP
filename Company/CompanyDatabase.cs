using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        public Company GetByID(int id)
        {
            foreach (var c in companyList)
            {
                if (c.Id == id)
                {
                    return c;
                }

            }
            return null;
        }


    }
}

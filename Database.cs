using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        static Database instance { get; set; } = new Database();
        List<Company> companyList = new List<Company>();
    }
}

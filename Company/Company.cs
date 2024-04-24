using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Company
    {
        public int Id;
        public string CompanyName { get; set; } = "";

        public string Vejnavn { get; set; } = "";

        public string Husnummer { get; set; } = "";

        public string Postnummer { get; set; } = "";
        public string By { get; set; } = "";

        public string Land { get; set; } = "";

        public string Valuta { get; set; } = "";


    }
}

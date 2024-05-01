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
        public DateTime Gennemførelsestidspunkt { get; set; }
        public int Kundenummer { get; set; }
        public OrderStatus Tilstand { get; set; }
        public List<Orderline> Ordrelinjer { get; set; }
        public decimal Ordrebeløb => Ordrelinjer.Sum(ol => ol.Pris * ol.Antal);

        public SalesOrderHeader(int ordrenummer, DateTime oprettelsestidspunkt, DateTime gennemførelsestidspunkt,
            int kundenummer, OrderStatus tilstand, List<Orderline> ordrelinjer)
        {
            OrderNumber = ordrenummer;
            Creationstime = oprettelsestidspunkt;
            Gennemførelsestidspunkt = gennemførelsestidspunkt;
            Kundenummer = kundenummer;
            Tilstand = tilstand;
            Ordrelinjer = ordrelinjer;
        }
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

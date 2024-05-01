using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Salgsordrehoved
    {
        public int Ordrenummer { get; set; }
        public DateTime Oprettelsestidspunkt { get; set; }
        public DateTime Gennemførelsestidspunkt { get; set; }
        public int Kundenummer { get; set; }
        public OrderStatus Tilstand { get; set; }
        public List<Ordrelinje> Ordrelinjer { get; set; }
        public decimal Ordrebeløb => Ordrelinjer.Sum(ol => ol.Pris * ol.Antal);

        public Salgsordrehoved(int ordrenummer, DateTime oprettelsestidspunkt, DateTime gennemførelsestidspunkt,
            int kundenummer, OrderStatus tilstand, List<Ordrelinje> ordrelinjer)
        {
            Ordrenummer = ordrenummer;
            Oprettelsestidspunkt = oprettelsestidspunkt;
            Gennemførelsestidspunkt = gennemførelsestidspunkt;
            Kundenummer = kundenummer;
            Tilstand = tilstand;
            Ordrelinjer = ordrelinjer;
        }
    }

    public class Ordrelinje
    {
        public string Vare { get; set; }
        public decimal Pris { get; set; }
        public int Antal { get; set; }

        public Ordrelinje(string vare, decimal pris, int antal)
        {
            Vare = vare;
            Pris = pris;
            Antal = antal;
        }
    }
}

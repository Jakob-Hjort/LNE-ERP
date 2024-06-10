using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderLinesDetails : Screen
    {
        public override string Title { get; set; } = "Sale Order Details";
        Orderline orderline { get; set; }

        public SalesOrderLinesDetails(Orderline orderline)
        {
            Title = "Order Lines for " + orderline.Vare;
            this.orderline = orderline;
        }
        protected override void Draw()
        {
            Console.WriteLine("Press F3 to go back");
            AddKey(ConsoleKey.F3, () =>
            {

            });
            ExitOnEscape();
        }
    }
}

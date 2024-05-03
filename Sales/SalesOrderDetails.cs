using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class SalesOrderDetails : Screen
    {
        public override string Title { get; set; } = "Sales order";
        SalesOrder SalesOrder = new();
        SalesOrderHeader SalesOrderHeader;

        public SalesOrderDetails(SalesOrder salesOrder)
        {
            Title = "Detaljer for " + salesOrder.SalesOrderName;
            this.SalesOrder = salesOrder;
        }

        protected override void Draw()
        {

            Console.WriteLine(SalesOrder.SalesOrderId);
            Console.WriteLine($"Kundenummer: {SalesOrder.CustomerId}");
            Console.WriteLine($"Navn: {SalesOrder.FullName}");
            Console.WriteLine("Price: {0}", SalesOrder.Prices);

            Console.WriteLine("Tryk F2 for at redigere");
            AddKey(ConsoleKey.F2, () =>
            {
                Screen.Display(new SalesOrderEditor(SalesOrder));
            });
            ExitOnEscape();
        }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP.Sales
{
    internal class SalesOrderLinesEditor : Screen
    {
        public override string Title { get; set; } = "Sale Order Editor";
        SalesOrderHeader SalesOrder {  get; set; }
       // SalesOrderHeader SalesOrderHeader { get;}

        public SalesOrderLinesEditor(SalesOrderHeader salesOrder)
        {
            Title = "Create for " + salesOrder.CustomerId;
            this.SalesOrder = salesOrder;
        }
        protected override void Draw()
        {
            ExitOnEscape();
            Form<Orderline> form = new();

            form.TextBox("Gennemførelse", nameof(Orderline.Vare));

            if (form.Edit(SalesOrder))
            {
                if (SalesOrder.OrderNumber != 0)
                {
                    Database.instance.UpdateSalesOrderLines(SalesOrder);
                }
                else
                {
                    Database.instance.InserSalesOrderList(SalesOrder);
                }
            }
        }
    }
}

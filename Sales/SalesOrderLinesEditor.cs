using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    internal class SalesOrderLinesEditor : Screen 
    {

        //Properties
        public override string Title { get; set; } = "Salgs ordrer linje";

        Orderline orderline = new();
        int OrderNumber;

        //Konstruktor
        public SalesOrderLinesEditor(Orderline line, int OrderNumber)
        {
            Title = "linje for salgsorder" + line.Vare;
            this.orderline = line;
            this.OrderNumber = OrderNumber;
            
        }
        protected override void Draw()
        {
            ExitOnEscape();
            Form<Orderline> form = new();

            form.SelectBox("Produkt", nameof(Orderline.Vare));
            var products = Database.instance.GetProducts();
            foreach (var product in products)
            {
                form.AddOption("Produkt", product.Name , product.Name);
            }

            form.TextBox("Antal", nameof(Orderline.Antal));

            if (form.Edit(orderline))
            {
                if (orderline.OrderLineID != 0)
                {
                    Database.instance.UpdateSalesOrderLine(orderline);
                }
                else
                {
                    Database.instance.InsertSalesOrderLine(orderline, OrderNumber);
                }
            }
        }
    }
}

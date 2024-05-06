using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class ProductsListPage : Screen
    {
        //Set the title of this page
        public override string Title { get; set; } = "Produkter";

        protected override void Draw()
        {
            ExitOnEscape();

            Console.CursorVisible = false;

            //A ListPage draws columns and rows in a box
            ListPage<Product> listPage = new();

            //V4 - Add keys to create company
            listPage.AddKey(ConsoleKey.F1, createNewProduct);
            Console.WriteLine("Tryk F1 for at oprette produkt");

            //V5 - Add key to edit company
            listPage.AddKey(ConsoleKey.F2, editProduct);
            Console.WriteLine("Tryk F2 for at redigere produkt");

            //V6 - Add key to edit company
            listPage.AddKey(ConsoleKey.F5, deleteProduct);
            Console.WriteLine("Tryk F5 for at slette produkt");

            //Add some columns
            listPage.AddColumn("produkt", nameof(Product.Name), 20);
            listPage.AddColumn("Units", nameof(Product.Units));
            listPage.AddColumn("Price", nameof(Product.Saleprice), 8);
            listPage.AddColumn("%", nameof(Product.AvanceiProcent), 7);

            //Get companies from the database and add them to the list
            var products = Database.instance.GetProducts();
            foreach (Product model in products)
            {
                listPage.Add(model);
            }


            //Enable selection of a company by using arrow keys
            var product = listPage.Select();
            if (product != null)
            {
                //Ask Screen class to display the CompanyDetails view
                Screen.Display(new ProductsDetails(product));
            }
        }

        void createNewProduct(Product _)
        {
            Product new_product = new();
            Screen.Display(new ProductEditor(new_product));
        }

        void editProduct(Product product)
        {
            Screen.Display(new ProductEditor(product));
        }

        void deleteProduct(Product product)
        {
            Database.instance.DeleteProduct(product);
            Screen.Clear(this);
            Draw();
        }
    }

}

using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class CompanyListPage : Screen
    {
        //Set the title of this page
        public override string Title { get; set; } = "Selskaber";

        protected override void Draw()
        {
            ExitOnEscape();

            Console.CursorVisible = false;

            //A ListPage draws columns and rows in a box
            ListPage<Company> listPage = new();

            //V4 - Add keys to create company
            listPage.AddKey(ConsoleKey.F1, createNewCompany);
            Console.WriteLine("Tryk F1 for at oprette virksomhed");

            //V5 - Add key to edit company
            listPage.AddKey(ConsoleKey.F2, editCompany);
            Console.WriteLine("Tryk F2 for at redigere virksomhed");

            //V6 - Add key to edit company
            listPage.AddKey(ConsoleKey.F5, removeCompany);
            Console.WriteLine("Tryk F5 for at slette virksomhed");

            //Add some columns
            listPage.AddColumn("Selskab", nameof(Company.CompanyName), 20);
            listPage.AddColumn("Land", nameof(Company.Country));
            listPage.AddColumn("Valuta", nameof(Company.Currency), 8);

            //Get companies from the database and add them to the list
            var companies = Database.instance.GetCompanies();
            foreach (Company model in companies)
            {
                listPage.Add(model);
            }
       

            //Enable selection of a company by using arrow keys
            var company = listPage.Select();
            if (company != null)
            {
                //Ask Screen class to display the CompanyDetails view
                Screen.Display(new CompanyDetails(company));
            }
        }

        void createNewCompany(Company _)
        {
            Company new_company = new();
            Screen.Display(new CompanyEditor(new_company));
        }

        void editCompany(Company company)
        {
            Screen.Display(new CompanyEditor(company));
        }

        void removeCompany(Company company)
        {
            Database.instance.DeleteCompany(company);
            Screen.Clear(this);
            Draw();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class CompanyEditor : Screen
    {
        public override string Title { get; set; } = "Selskab";
        Company company = new();

        public CompanyEditor(Company company)
        {
            Title = "Redigerer for " + company.CompanyName;
            this.company = company;
        }

        protected override void Draw()
        {
            ExitOnEscape();
            Form<Company> form = new();

            form.TextBox("Name", nameof(Company.CompanyName));
            form.TextBox("Adresse", nameof(Company.StreetName));
            form.TextBox("City", nameof(Company.City));
            form.TextBox("Country", nameof(Company.Country));
            form.SelectBox("Currency", nameof(Company.Currency));
            form.AddOption("Currency", "DKK", Currency.DKK);
            form.AddOption("Currency", "EUR", Currency.EUR);
            form.AddOption("Currency", "USD", Currency.USD);
            form.AddOption("Currency", "SEK", Currency.SEK);
            if (form.Edit(company))
            {
                if (company.CompanyId != 0)
                {
                    Database.instance.UpdateCompany(company);
                }
                else
                {
                    Database.instance.InsertCompany(company);
                }
                Console.WriteLine("Ændringerne blev gemt");
            }
            else
            {
                Console.WriteLine("Ingen ændringer");
            }


        }
    }
}

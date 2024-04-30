using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP;


    public class CompanyDetails : Screen
{
    public override string Title { get; set; } = "Selskab";
    Company company = new();

    public CompanyDetails(Company company)
    {
        Title = "Detaljer for " + company.CompanyName;
        this.company = company;
    }

    protected override void Draw()
    {

        Console.WriteLine(company.CompanyName);
        Console.WriteLine("Adresse:");
        Console.WriteLine("{0} {1}", company.StreetName, company.Number);
        Console.WriteLine("{0} {1}", company.City, company.Country);
        Console.WriteLine("Valuta: {0}", company.Currency);

        Console.WriteLine("Tryk F2 for at redigere");
        AddKey(ConsoleKey.F2, () => {
            Screen.Display(new CompanyEditor(company));
        });
        ExitOnEscape();
    }
}


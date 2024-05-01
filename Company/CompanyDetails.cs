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
        Console.WriteLine($"ID:{company.CompanyId}");
        Console.WriteLine($"Name:{company.CompanyName}");
        Console.WriteLine($"Adresse:{company.StreetName}");
        Console.WriteLine($"House Number:{company.HouseNumber}");
        Console.WriteLine($"City:{company.City}");
        Console.WriteLine($"Country:{company.Country}");
        Console.WriteLine($"Valuta:{company.Currency}");

        Console.WriteLine("Tryk F2 for at redigere");
        AddKey(ConsoleKey.F2, () => {
            Screen.Display(new CompanyEditor(company));
        });
        ExitOnEscape();
    }
}


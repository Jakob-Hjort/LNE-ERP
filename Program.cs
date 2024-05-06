using TECHCOOL.UI;
using System;
using System.Data.Common;

namespace LNE_ERP
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testdata udkommenter når vi kobler database på
            Database.instance.Testdata();

            Database.instance.CustomerTestdata();

            //Show main menu
            Screen.Display(new MainMenu());
            
        }
    }
}
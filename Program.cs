using TECHCOOL.UI;
using System;
using System.Data.Common;

namespace LNE_ERP
{
    class Program
    {
        static void Main(string[] args)
        {

            Database.instance.Testdata();

            //Show main menu
            Screen.Display(new MainMenu());
            
        }
    }
}
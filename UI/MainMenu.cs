﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace LNE_ERP
{
    public class MainMenu : Screen
    {
        public override string Title { get; set; } = "LNE_ERP Software";
        protected override void Draw()
        {
            {
                //Print som text
                Console.WriteLine("Velkommen til LNE_ERP");
                Console.WriteLine("Vær venlig og vælge et modul for at fortsætte!");

                Menu menu = new();
                menu.Add(new CompanyListPage());
                menu.Add(new SalesOrderListPage());
                menu.Add(new CustomerListPage());
                menu.Add(new ProductsListPage());
                menu.Start(this);
            }
        }
    }
}

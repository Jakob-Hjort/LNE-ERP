﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Person
    {

        public Person()
        {
            Addresses = new Addresses();
        }

        public int PersonID { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Addresses Addresses { get; set; }

        public string FullName
        {
            get { return $"{FirstName}  {LastName}"; }
        }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

    }
}

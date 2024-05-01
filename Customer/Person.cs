using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public class Person
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public string Contactinfo { get; set; }

        public string Fullname
        {
            get { return $"{Firstname}  {Lastname}"; }
            set { }
        }


        public Person(string firstname, string lastname, string address, string contactinfo, string fullname)
        {
            Firstname = firstname;
            Lastname = lastname;
            Address = address;
            Contactinfo = contactinfo;
            Fullname = fullname;

        }

    }
}

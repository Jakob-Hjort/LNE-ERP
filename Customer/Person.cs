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

        public Addresses Addresses { get; set; }

        public string Fullname
        {
            get { return $"{Firstname}  {Lastname}"; }
        }
        public string Email { get; set; }

        public int PhoneNumber { get; set; }


       /* public Person(string firstname, string lastname, string address, string contactinfo, string email,int phonenumber)
        {
            Firstname = firstname;
            Lastname = lastname;
            Address = address;
            Contactinfo = contactinfo;
            Email = email;
            PhoneNumber = phonenumber;


        }*/

    }
}

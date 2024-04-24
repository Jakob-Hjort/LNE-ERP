using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        public Company GetByID(int id)
        {
            foreach (var c in companyList)
            {
                if (c.Id == id)
                {
                    return c;
                }

            }
            return null;
        }

        public List<Company> GetAllCompanies()
        {
            return new List<Company>(companyList); // Return a new list to avoid exposing the original list
        }

        public void InsertCompany(Company company)
        {
            if (company.Id == 0)
            {
                companyList.Add(company);
            }
            else
            {
                Console.WriteLine($"You cannot add this {company}, because its alrdy made");
            }
        }

        public void UpdateCompany(Company updatedCompany)
        {
            if (updatedCompany.Id != 0)
            {
                int index = companyList.FindIndex(c => c.Id == updatedCompany.Id);
                if (index != -1)
                {
                    companyList[index] = updatedCompany;
                }
                // Optionally, you could throw an exception or handle the case where company with given ID is not found
            }
            // Optionally, you could throw an exception or handle the case where ID is 0
        }

        public void DeleteCompanyById(int id)
        {
            companyList.RemoveAll(c => c.Id == id);
        }
    }
}

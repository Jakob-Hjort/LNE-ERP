using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
        public partial class Database
        {
            List<Company> companies = new()
            {
            new Company {CompanyId = 1, CompanyName = "Foderbrættet A/S",StreetName = "Øster Uttrup",HouseNumber = "2",ZipCode = "9000",City = "Aalborg",Country = "Danmark", Currency = Currency.DKK},
            //new Company {CompanyId = 2, CompanyName = "Foodboard Ltd", Country = "USA", Currency = Currency.USD}
            };
            public Company GetCompanyById(int id)
            {
                foreach (var company in companies)
                {
                    if (company.CompanyId == id)
                    {
                        return company;
                    }
                }
                return null;
            }

            public List<Company> GetCompanies()
            {
                List<Company> companyCopy = new();
                companyCopy.AddRange(companies);
                return companyCopy;
            }

            public void InsertCompany(Company company)
            {
                if (company.CompanyId != 0)
                {
                    return;
                }
                company.CompanyId = companies.Count+1;
                companies.Add(company);
            }

            public void UpdateCompany(Company company)
            {
                if (company.CompanyId == 0)
                {
                    return;
                }

                for (var i = 0; i < companies.Count; i++)
                {
                    if (companies[i].CompanyId == company.CompanyId)
                    {
                        companies[i] = company;
                    }
                }
            }

            public void DeleteCompany(Company company)
            {
                if (company.CompanyId == 0)
                {
                    return;
                }
                if (companies.Contains(company))
                {
                    companies.Remove(company);
                }
            }
        }
    }

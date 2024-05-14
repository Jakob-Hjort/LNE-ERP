using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        //public List<Company> GetCompanies()
        //{

        //    List<Company> companyCopy = new();
        //    companyCopy.AddRange(companies);
        //    return companyCopy;
        //}

        public List<Company> GetCompanies()
        {

            List<Company> companyList = new();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string sql = "Select CompanyId, CompanyName, CompanyStreet FROM companies";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Company company = new();
                        company.CompanyId = reader.GetInt32(0);
                        company.CompanyName = reader.GetString(1);
                        company.StreetName = reader.GetString(2);
                        company.Currency = (Currency)reader.GetInt32(3);
                        companyList.Add(company);

                    }
                }

            }

            return companyList;
        }

        public void InsertCompany(Company company)
        {
            if (company.CompanyId != 0)
            {
                return;
            }


            using (var conn = getConnection())
            {
                string sql = "INSERT INTO .....";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();

                //get id
                command = conn.CreateCommand();
                command.CommandText = "SELECT SCOPE_IDENTITY()";
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                company.CompanyId = reader.GetInt32(0);
            }

            company.CompanyId = companies.Count + 1;
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

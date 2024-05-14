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
        List<Company> companies = new();

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

            List<Company> companyList = new();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string sql = "Select CompanyId, CompanyName, StreetName, HouseNumber, ZipCode, City, Country, Currency FROM companies";
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
                        company.HouseNumber = reader.GetString(3);
                        company.ZipCode = reader.GetString(4);
                        company.City = reader.GetString(5);
                        company.Country = reader.GetString(6);
                        company.Currency = (Currency)reader.GetInt32(7);
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
                conn.Open();
                string sql = "INSERT INTO companies (CompanyName, StreetName, HouseNumber, ZipCode, City, Country, Currency) VALUES (@CompanyName, @StreetName, @HouseNumber, @ZipCode, @City, @Country, @Currency)";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@StreetName", company.StreetName);
                command.Parameters.AddWithValue("@HouseNumber", company.HouseNumber);
                command.Parameters.AddWithValue("@ZipCode", company.ZipCode);
                command.Parameters.AddWithValue("@City", company.City);
                command.Parameters.AddWithValue("@Country", company.Country);
                command.Parameters.AddWithValue("@Currency", company.Currency);
                try
                {
                    command.ExecuteNonQuery();
                    //get id
                    command = conn.CreateCommand();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    company.CompanyId = reader.GetInt32(0);

                }catch (Exception ex) { 
                    Console.WriteLine(ex.Message);
                }

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

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
        public Company GetCompanyById(int id)
        {
            foreach (var company in companyList)
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
            companyList = new();
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
                string sql = "INSERT INTO companies (CompanyName, StreetName, HouseNumber, ZipCode, City, Country, Currency) VALUES (@CompanyName, @StreetName, @HouseNumber, @ZipCode, @City, @Country, @Currency); SELECT SCOPE_IDENTITY()";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@StreetName", company.StreetName);
                command.Parameters.AddWithValue("@HouseNumber", company.HouseNumber);
                command.Parameters.AddWithValue("@ZipCode", company.ZipCode);
                command.Parameters.AddWithValue("@City", company.City);
                command.Parameters.AddWithValue("@Country", company.Country);
                command.Parameters.AddWithValue("@Currency", company.Currency);
                
                company.CompanyId = Convert.ToInt32(command.ExecuteScalar());

                //try
                //{
                //    command.ExecuteNonQuery();
                //    //get id
                //    command = conn.CreateCommand();
                //    command.CommandText = "";
                //    SqlDataReader reader = command.ExecuteReader();
                //    reader.Read();
                //    company.CompanyId = reader.GetInt32(0);

                //}catch (Exception ex) { 
                //    Console.WriteLine(ex.Message);
                //}

            }

            company.CompanyId = companyList.Count + 1;
            companyList.Add(company);

        }

        public void UpdateCompany(Company company)
        {
            if (company.CompanyId == 0)
            {
                return;
            }

            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "UPDATE companies SET CompanyName = @CompanyName, StreetName = @StreetName, HouseNumber = @HouseNumber, ZipCode = @ZipCode, City = @City, Country = @Country, Currency = @Currency WHERE CompanyId = @CompanyId";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@StreetName", company.StreetName);
                command.Parameters.AddWithValue("@HouseNumber", company.HouseNumber);
                command.Parameters.AddWithValue("@ZipCode", company.ZipCode);
                command.Parameters.AddWithValue("@City", company.City);
                command.Parameters.AddWithValue("@Country", company.Country);
                command.Parameters.AddWithValue("@Currency", company.Currency);
                command.Parameters.AddWithValue("@CompanyId", company.CompanyId);

                // Opdater virksomheden i databasen
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Opdater virksomheden i listen, hvis den blev opdateret i databasen
                    for (int i = 0; i < companyList.Count; i++)
                    {
                        if (companyList[i].CompanyId == company.CompanyId)
                        {
                            companyList[i] = company;
                            break; // Vi har fundet og opdateret virksomheden, så vi kan afslutte løkken
                        }
                    }
                }
            }
        }

        public void DeleteCompany(Company company)
        {
            if (company.CompanyId == 0)
            {
                return;
            }
            using (var conn = getConnection())
            {
                conn.Open();
                string sql = "DELETE FROM companies WHERE CompanyId = @CompanyId";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@CompanyId", company.CompanyId);

                // Fjern virksomheden fra databasen
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Fjern virksomheden fra listen, hvis den blev slettet fra databasen
                    if (companyList.Contains(company))
                    {
                        companyList.Remove(company);
                    }
                }
            }
        }
    }
}

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
        // En liste til at gemme virksomheder i hukommelsen
        List<Company> companies = new();

        // Metode til at hente en virksomhed baseret på dens id
        public Company GetCompanyById(int id)
        {
            // Gennemgår hver virksomhed i listen og returnerer den, hvis id'et matcher
            foreach (var company in companies)
            {
                if (company.CompanyId == id)
                {
                    return company;
                }
            }
            return null; // Returnerer null, hvis ingen virksomhed har det pågældende id
        }
        public List<Company> GetCompanies() // Metode til at hente alle virksomheder fra databasen
        {
            // Opretter en liste til at gemme virksomhederne
            List<Company> companyList = new();

            // Opretter og åbner en forbindelse til databasen
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                // SQL-forespørgsel til at hente virksomheder
                string sql = "Select CompanyId, CompanyName, StreetName, HouseNumber, ZipCode, City, Country, Currency FROM companies";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

                // Læser data fra databasen
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Opretter en ny virksomhed og fylder dens felter med data fra databasen
                        Company company = new();
                        company.CompanyId = reader.GetInt32(0);
                        company.CompanyName = reader.GetString(1);
                        company.StreetName = reader.GetString(2);
                        company.HouseNumber = reader.GetString(3);
                        company.ZipCode = reader.GetString(4);
                        company.City = reader.GetString(5);
                        company.Country = reader.GetString(6);
                        company.Currency = (Currency)reader.GetInt32(7);
                        companyList.Add(company); // Tilføjer virksomheden til listen

                    }
                }

            }

            return companyList; // Returnerer listen af virksomheder
        }

        public void InsertCompany(Company company) // Metode til at indsætte en ny virksomhed i databasen
        {
            // Returnerer, hvis virksomhedens id ikke er 0 (dvs. den allerede findes)
            if (company.CompanyId != 0)
            {
                return;
            }

            // Opretter og åbner en forbindelse til databasen
            using (var conn = getConnection())
            {
                conn.Open();
                // SQL-indsættelsesforespørgsel
                string sql = "INSERT INTO companies (CompanyName, StreetName, HouseNumber, ZipCode, City, Country, Currency) VALUES (@CompanyName, @StreetName, @HouseNumber, @ZipCode, @City, @Country, @Currency)";
                SqlCommand command = new SqlCommand(sql, conn);
                // Tilføjer parametre til forespørgslen
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@StreetName", company.StreetName);
                command.Parameters.AddWithValue("@HouseNumber", company.HouseNumber);
                command.Parameters.AddWithValue("@ZipCode", company.ZipCode);
                command.Parameters.AddWithValue("@City", company.City);
                command.Parameters.AddWithValue("@Country", company.Country);
                command.Parameters.AddWithValue("@Currency", company.Currency);
                try
                {
                    command.ExecuteNonQuery(); // Udfører indsættelsesforespørgslen
                    // Henter det nyligt indsatte virksomheds id
                    command = conn.CreateCommand();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    company.CompanyId = reader.GetInt32(0);

                }catch (Exception ex) { 
                    Console.WriteLine(ex.Message); // Udskriver fejlmeddelelsen, hvis der opstår en undtagelse
                }

            }

            // Tildeler et id og tilføjer virksomheden til listen
            company.CompanyId = companies.Count + 1;
            companies.Add(company);

        }

        public void UpdateCompany(Company company) // Metode til at opdatere en eksisterende virksomhed
        {
            // Returnerer, hvis virksomhedens id er 0 (dvs. den ikke findes)
            if (company.CompanyId == 0)
            {
                return;
            }

            // Opretter og åbner en forbindelse til databasen
            using (var conn = getConnection())
            {
                conn.Open();
                // SQL-opdateringsforespørgsel
                string sql = "UPDATE companies SET CompanyName = @CompanyName, StreetName = @StreetName, HouseNumber = @HouseNumber, ZipCode = @ZipCode, City = @City, Country = @Country, Currency = @Currency WHERE CompanyId = @CompanyId";
                SqlCommand command = new SqlCommand(sql, conn);

                // Tilføjer parametre til forespørgslen
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@StreetName", company.StreetName);
                command.Parameters.AddWithValue("@HouseNumber", company.HouseNumber);
                command.Parameters.AddWithValue("@ZipCode", company.ZipCode);
                command.Parameters.AddWithValue("@City", company.City);
                command.Parameters.AddWithValue("@Country", company.Country);
                command.Parameters.AddWithValue("@Currency", company.Currency);
                command.Parameters.AddWithValue("@CompanyId", company.CompanyId);

                // Udfører opdateringsforespørgslen
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Opdaterer virksomheden i listen, hvis den blev opdateret i databasen
                    for (int i = 0; i < companies.Count; i++)
                    {
                        if (companies[i].CompanyId == company.CompanyId)
                        {
                            companies[i] = company;
                            break; // Afslutter løkken, da virksomheden er fundet og opdateret
                        }
                    }
                }
            }
        }

        public void DeleteCompany(Company company) //Metode til at slette en virksomged
        {
            // Returnerer, hvis virksomhedens id er 0 (dvs. den ikke findes)
            if (company.CompanyId == 0)
            {
                return;
            }

            // Opretter og åbner en forbindelse til databasen
            using (var conn = getConnection())
            {
                conn.Open();
                // SQL-sletningsforespørgsel
                string sql = "DELETE FROM companies WHERE CompanyId = @CompanyId";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@CompanyId", company.CompanyId);

                // Fjerner virksomheden fra listen, hvis den blev slettet fra databasen
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Fjern virksomheden fra listen, hvis den blev slettet fra databasen
                    if (companies.Contains(company))
                    {
                        companies.Remove(company);
                    }
                }
            }
        }
    }
}

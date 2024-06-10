using System.Data.SqlClient;

namespace LNE_ERP
{
    public partial class Database
    {
        public Company? GetCompanyById(int id)
        {
            foreach (var company in companyList)
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
            companyList = new();
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

            
            using (var conn = getConnection())
            {
                conn.Open();

                // SQL-forespørgsel til at indsætte en virksomhed
                string sql = "INSERT INTO companies (CompanyName, StreetName, HouseNumber, ZipCode, City, Country, Currency) " +
                             "VALUES (@CompanyName, @StreetName, @HouseNumber, @ZipCode, @City, @Country, @Currency); SELECT SCOPE_IDENTITY()";

                SqlCommand command = new SqlCommand(sql, conn);
                // Tilføjer parametre til forespørgslen
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@StreetName", company.StreetName);
                command.Parameters.AddWithValue("@HouseNumber", company.HouseNumber);
                command.Parameters.AddWithValue("@ZipCode", company.ZipCode);
                command.Parameters.AddWithValue("@City", company.City);
                command.Parameters.AddWithValue("@Country", company.Country);
                command.Parameters.AddWithValue("@Currency", company.Currency);
                
                company.CompanyId = Convert.ToInt32(command.ExecuteScalar());

            }

            company.CompanyId = companyList.Count + 1;
            companyList.Add(company);

        }

        public void UpdateCompany(Company company) // Metode til at opdatere en eksisterende virksomhed
        {
            // Returnerer, hvis virksomhedens id er 0 (dvs. den ikke findes)
            if (company.CompanyId == 0)
            {
                return;
            }

            using (var conn = getConnection())
            {
                conn.Open();

                // SQL- Forforespørgsel til at opdatere en virksomhed
                string sql = "UPDATE companies SET CompanyName = @CompanyName, StreetName = @StreetName, " +
                             "HouseNumber = @HouseNumber, ZipCode = @ZipCode, City = @City, Country = @Country, Currency = @Currency " +
                             "WHERE CompanyId = @CompanyId";

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
                    // Opdater virksomheden i listen, hvis den blev opdateret i databasen
                    for (int i = 0; i < companyList.Count; i++)
                    {
                        if (companyList[i].CompanyId == company.CompanyId)
                        {
                            companyList[i] = company;
                            break;
                        }
                    }
                }
            }
        }

        public void DeleteCompany(Company company) //Metode til at slette en Virksomhed
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
                    if (companyList.Contains(company))
                    {
                        companyList.Remove(company);
                    }
                }
            }
        }
    }
}

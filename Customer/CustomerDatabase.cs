using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LNE_ERP
{
    public partial class Database
    {
        List<Customer> customers = new();

        public Customer? GetCustomerById(int id)
        {
            foreach (var customer in customerlist)
            {
                if (customer.CustomerID == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public List<Customer> GetCustomer()
        {
            List<Customer> customerList = new();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string sql = "Select CustomerID, Person.PersonID, FirstName, LastName, Email, PhoneNumber, Streetname, Housenumber, City, Postalcode, Person.AddressID ,LastBuy FROM Person inner join Addresses on Person.AddressID = Addresses.AddressID inner join Customer on Customer.PersonID = Person.PersonID ";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    
                        Addresses addresses = new();
                        addresses.Streetname = reader.GetString(6);
                        addresses.Housenumber = reader.GetString(7);
                        addresses.City = reader.GetString(8);
                        addresses.Postalcode = reader.GetInt32(9);
                        addresses.AddressID = reader.GetInt32(10);


                        Customer customer = new()
                        {
                            Addresses = addresses
                        };

                        // Customer customer = new();
                        customer.CustomerID = reader.GetInt32(0);
                        customer.PersonID = reader.GetInt32(1);
                        customer.FirstName = reader.GetString(2);
                        customer.LastName = reader.GetString(3);
                        customer.Email = reader.GetString(4);
                        customer.PhoneNumber = reader.GetString(5);
                        // customer.Addresses = addresses;
                        customerList.Add(customer);

                        customer.LastPurchaseDate = reader.GetDateTime(11);
                    }
                }
            }
            return customerList;



        }

        public void InsertCustomer(Customer customer)
        {
            if (customer.PersonID != 0)
            {
                return;
            }

            using (var conn = getConnection())
            {
                conn.Open();
                string sql;
                int addressId = 0;

                // Indsæt adresse, hvis tilgængelig, og få AddressID
                if (customer.Addresses != null)
                {
                    sql = "INSERT INTO Addresses (Streetname, Housenumber, Postalcode, City) VALUES (@Streetname, @Housenumber, @Postalcode, @City); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@Streetname", customer.Addresses.Streetname);
                        command.Parameters.AddWithValue("@Housenumber", customer.Addresses.Housenumber);
                        command.Parameters.AddWithValue("@Postalcode", customer.Addresses.Postalcode);
                        command.Parameters.AddWithValue("@City", customer.Addresses.City);

                        addressId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                // Indsæt person og få PersonID
                sql = "INSERT INTO Person (FirstName, LastName, Email, PhoneNumber, AddressID) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @AddressID); SELECT SCOPE_IDENTITY();";
                int personId;
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@AddressID", addressId);

                    personId = Convert.ToInt32(command.ExecuteScalar());
                    customer.PersonID = personId;
                }

                if (customer.LastPurchaseDate < new DateTime(1753, 1, 1) || customer.LastPurchaseDate > new DateTime(9999, 12, 31))
                {
                    customer.LastPurchaseDate = DateTime.Now; 
                }

                // Indsæt kunde ved hjælp af det opnåede PersonID
                sql = "INSERT INTO Customer (PersonID, Fullname, LastBuy) VALUES (@PersonID, @Fullname, @LastBuy)";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@PersonID", personId);
                    command.Parameters.AddWithValue("@Fullname", customer.FullName);
                    command.Parameters.AddWithValue("@LastBuy", customer.LastPurchaseDate);
                    command.ExecuteNonQuery();
                }
            }

            customers.Add(customer);
        }




        public void UpdateCustomer(Customer customer)
        {
            if (customer.CustomerID == 0)
            {
                return;
            }

            using (var conn = getConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Opdater Addresse tabellen
                    string sql = "UPDATE Addresses SET Streetname = @Streetname, Housenumber = @Housenumber, Postalcode = @Postalcode, City = @City WHERE AddressID = @AddressID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@Streetname", customer.Addresses.Streetname);
                        command.Parameters.AddWithValue("@Housenumber", customer.Addresses.Housenumber);
                        command.Parameters.AddWithValue("@Postalcode", customer.Addresses.Postalcode);
                        command.Parameters.AddWithValue("@City", customer.Addresses.City);
                        command.Parameters.AddWithValue("@AddressID", customer.Addresses.AddressID);
                        command.ExecuteNonQuery();
                    }

                    // Opdater Person tabellen
                    sql = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                        command.Parameters.AddWithValue("@PersonID", customer.PersonID);
                        command.ExecuteNonQuery();
                    }

                    // Opdater kunde tabellen
                    sql = "UPDATE Customer SET Fullname = @Fullname, LastBuy = @LastBuy WHERE CustomerID = @CustomerID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@Fullname", customer.FullName);
                        command.Parameters.AddWithValue("@LastBuy", customer.LastPurchaseDate);
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        command.ExecuteNonQuery();
                    }

                    
                    transaction.Commit();

                    // Opdater kunden i den interne liste
                    for (var i = 0; i < customers.Count; i++)
                    {
                        if (customers[i].CustomerID == customer.CustomerID)
                        {
                            customers[i] = customer;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("An error occurred: " + ex.Message);
                    Console.ReadLine();  // Hold konsollen åben for at læse fejlmeddelelsen
                }
            }
        }


        public void DeleteCustomer(Customer customer)
        {
            if (customer.CustomerID == 0)
            {
                throw new ArgumentException("CustomerID is 0, nothing to delete.");
            }

            using (var conn = getConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Slet Ordrelinjer knyttet til kundens ordrer
                    string sql = @"
                            DELETE OL
                            FROM OrderLines OL
                            INNER JOIN SalesOrderHeader O ON OL.OrderNumber = O.OrderNumber
                            WHERE O.CustomerID = @CustomerID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Deleted {rowsAffected} rows from OrderLines table for CustomerID: {customer.CustomerID}");
                    }

                    // Slet ordrer tilknyttet kunden
                    sql = "DELETE FROM SalesOrderHeader WHERE CustomerID = @CustomerID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        int rowsAffected = command.ExecuteNonQuery();
                        
                    }

                    // Slet kunde tabellen
                    sql = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customer.CustomerID);
                        int rowsAffected = command.ExecuteNonQuery();
                        
                    }

                    // Slet person tabellen
                    sql = "DELETE FROM Person WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@PersonID", customer.PersonID);
                        int rowsAffected = command.ExecuteNonQuery();
                        
                    }

                    // Tjek om adressen bruges af en anden person
                    bool isAddressInUse;
                    sql = "SELECT COUNT(*) FROM Person WHERE AddressID = @AddressID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@AddressID", customer.Addresses.AddressID);
                        isAddressInUse = ((int)command.ExecuteScalar() > 0);
                        
                    }

                    // Slet adresseposten, hvis den ikke er i brug
                    if (!isAddressInUse)
                    {
                        sql = "DELETE FROM Addresses WHERE AddressID = @AddressID";
                        using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                        {
                            command.Parameters.AddWithValue("@AddressID", customer.Addresses.AddressID);
                            int rowsAffected = command.ExecuteNonQuery();
                            
                        }
                    }

                   
                    transaction.Commit();


                    // Fjern kunden fra listen, hvis den blev slettet fra databasen
                    if (customers.Contains(customer))
                    {
                        customers.Remove(customer);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}



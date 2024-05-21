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

        public Customer GetCustomerById(int id)
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
                string sql = "Select CustomerID, Person.PersonID, FirstName, LastName, Email, PhoneNumber, Streetname, Housenumber, City, Postalcode FROM Person inner join Addresses on Person.AddressID = Addresses.AddressID inner join Customer on Customer.PersonID = Person.PersonID ";
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


                        Customer customer = new();
                        customer.CustomerID = reader.GetInt32(0);
                        customer.PersonID = reader.GetInt32(1);
                        customer.FirstName = reader.GetString(2);
                        customer.LastName = reader.GetString(3);
                        customer.Email = reader.GetString(4);
                        customer.PhoneNumber = reader.GetString(5);
                        customer.Addresses = addresses;
                        customerList.Add(customer);
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

                // Insert address if available and get AddressID
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

                // Insert person and get PersonID
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
                    customer.LastPurchaseDate = DateTime.Now; // or any other default date within the valid range
                }

                // Insert customer using the obtained PersonID
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

            for (var i = 0; i < customerlist.Count; i++)
            {
                if (customerlist[i].CustomerID == customer.CustomerID)
                {
                    customerlist[i] = customer;
                }
            }
        }

        public void DeleteCustomer(Customer customer)
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
                    // Delete the customer record
                    string sql = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customer.CustomerID);
                        command.ExecuteNonQuery();
                    }

                    // Get PersonID related to the deleted customer
                    int personId;
                    sql = "SELECT PersonID FROM Customer WHERE CustomerId = @CustomerId";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customer.CustomerID);
                        personId = (int)command.ExecuteScalar();
                    }

                    // Delete the person record
                    sql = "DELETE FROM Person WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@PersonID", personId);
                        command.ExecuteNonQuery();
                    }

                    // Get AddressID related to the deleted person
                    int addressId;
                    sql = "SELECT AddressID FROM Person WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@PersonID", personId);
                        addressId = (int)command.ExecuteScalar();
                    }

                    // Check if the address is used by any other person
                    bool isAddressInUse;
                    sql = "SELECT COUNT(*) FROM Person WHERE AddressID = @AddressID";
                    using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@AddressID", addressId);
                        isAddressInUse = ((int)command.ExecuteScalar() > 0);
                    }

                    // Delete the address record if not in use
                    if (!isAddressInUse)
                    {
                        sql = "DELETE FROM Addresses WHERE AddressID = @AddressID";
                        using (SqlCommand command = new SqlCommand(sql, conn, transaction))
                        {
                            command.Parameters.AddWithValue("@AddressID", addressId);
                            command.ExecuteNonQuery();
                        }
                    }

                    // Commit the transaction
                    transaction.Commit();

                    // Remove the customer from the list if it was successfully deleted from the database
                    if (customers.Contains(customer))
                    {
                        customers.Remove(customer);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}



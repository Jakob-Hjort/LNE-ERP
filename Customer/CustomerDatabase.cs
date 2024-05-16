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

        public Customer GetCustomerrById(int id)
        {
            foreach (var customer in customerlist)
            {
                if (customer.CustomerNumber == id)
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
                string sql = "Select PersonID, FirstName, LastName, Email, PhoneNumber FROM Person";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new();
                        customer.PersonID = reader.GetInt32(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                        customer.Email = reader.GetString(4);
                        //customer.PhoneNumber = reader.GetString(5);
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
                string sql = "INSERT INTO Person (FirstName, LastName, Email, PhoneNumber) VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                try
                {
                    // Få Id.
                    command.ExecuteNonQuery();
                    command = conn.CreateCommand();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    customer.PersonID = reader.GetInt32(0);
                    reader.Close();

                    // Indsæt adresseoplysninger, hvis de er tilgængelige
                    if (customer.Addresses != null)
                    {
                        sql = "INSERT INTO Addresses (Streetname, Housenumber, Postalcode, City) VALUES (@Streetname, @Housenumber, @Postalcode, @City)";
                        command.CommandText = sql;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Streetname", customer.Addresses.Streetname);
                        command.Parameters.AddWithValue("@Housenumber", customer.Addresses.Housenumber);
                        command.Parameters.AddWithValue("@Postalcode", customer.Addresses.Postalcode);
                        command.Parameters.AddWithValue("@City", customer.Addresses.City);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            customers.Add(customer);
        }


        public void UpdateCustomer(Customer customer)
        {
            if (customer.CustomerNumber == 0)
            {
                return;
            }

            for (var i = 0; i < customerlist.Count; i++)
            {
                if (customerlist[i].CustomerNumber == customer.CustomerNumber)
                {
                    customerlist[i] = customer;
                }
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            if (customer.CustomerNumber == 0)
            {
                return;
            }
            if (customerlist.Contains(customer))
            {
                customerlist.Remove(customer);
            }
        }
    }
}



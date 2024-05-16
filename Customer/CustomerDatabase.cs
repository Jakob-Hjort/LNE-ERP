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
                string sql = "Select PersonID, FirstName, LastName, Email, PhoneNumber, Streetname, Housenumber, City, Postalcode FROM Person inner join Addresses on Person.AddressID = Addresses.AddressID";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Addresses addresses = new();
                        addresses.Streetname = reader.GetString(5);
                        addresses.Housenumber = reader.GetString(6);
                        addresses.City = reader.GetString(7);
                        addresses.Postalcode = reader.GetInt32(8);


                        Customer customer = new();
                        customer.PersonID = reader.GetInt32(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                        customer.Email = reader.GetString(3);
                        customer.PhoneNumber = reader.GetString(4);
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
                int addresseId = 0;


                if (customer.Addresses != null)
                {
                    sql = "INSERT INTO Addresses (Streetname, Housenumber, Postalcode, City) VALUES (@Streetname, @Housenumber, @Postalcode, @City); SELECT SCOPE_IDENTITY()";
                    SqlCommand command1 = new SqlCommand(sql, conn);
                    //command.CommandText = sql;
                    command1.Parameters.Clear();
                    command1.Parameters.AddWithValue("@Streetname", customer.Addresses.Streetname);
                    command1.Parameters.AddWithValue("@Housenumber", customer.Addresses.Housenumber);
                    command1.Parameters.AddWithValue("@Postalcode", customer.Addresses.Postalcode);
                    command1.Parameters.AddWithValue("@City", customer.Addresses.City);
                    //command.ExecuteNonQuery();

                    addresseId = Convert.ToInt32(command1.ExecuteScalar());
                }

                sql = "INSERT INTO Person (FirstName, LastName, Email, PhoneNumber, AddressID) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @AddressID); SELECT SCOPE_IDENTITY()";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.Parameters.AddWithValue("@AddressID", addresseId);
                try
                {
                    
                    int personId = Convert.ToInt32(command.ExecuteScalar());
                    customer.PersonID = personId;

          
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



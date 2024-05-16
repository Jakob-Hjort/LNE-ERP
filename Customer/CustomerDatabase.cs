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
 

                    addresseId = Convert.ToInt32(command1.ExecuteScalar());
                }

                // inset person først og lad variable peje på PersonID i customer.

                sql = "INSERT INTO Person (FirstName, LastName, Email, PhoneNumber, AddressID) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @AddressID); SELECT SCOPE_IDENTITY()";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.Parameters.AddWithValue("@AddressID", addresseId);
                //command.Parameters.AddWithValue("@Fullname")
                try
                {
                    
                    int personId = Convert.ToInt32(command.ExecuteScalar());
                    customer.PersonID = personId;

          
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                sql = "INSERT INTO Customer (PersonID,Fullname,LastBuy";
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
            if (customerlist.Contains(customer))
            {
                customerlist.Remove(customer);
            }
        }
    }
}



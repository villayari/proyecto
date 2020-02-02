﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    class CustomersRepository : Repository<Customers>, ICustomersRepository
    {
        public CustomersRepository(string connectionString) : base(connectionString)
        {

        }

        public Customers GetById(string id)
        {
            using (var connection = new SqlConnection(_connectionString)) { 
                return connection.GetAll<Customers>().Where(
                customer => customer.CustomerID.Equals(id)).First();
            }
        }

        public bool Update(Customers customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                
                var result = connection.Execute("update Customers "+
                                            "set CompanyName = @company, "+
                                            "ContactName = @contact, " +
                                            "City = @city, " +
                                            "Country = @country, "+
                                            "Phone = @phone " +
                                            "where CustomerID = @myId", 
                                            new {company = customer.CompanyName,
                                                contact = customer.ContactName,
                                                city = customer.City,
                                                country = customer.Country,
                                                phone = customer.Phone,
                                                myId = customer.CustomerID});
                /*
                if (result > 0) return true;
                else return false;
                */
                return Convert.ToBoolean(result);
            }

        }


        public bool Delete(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute("delete from Customers " +
                    "where CustomerID = @myid ", new { myid = id});

                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<Customers> PagedList()
        {
            
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Customers>("dbo.uspCustomerPageList",
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("Select Count(CustomerID) from dbo.Customers");
            }
        }
    }
}

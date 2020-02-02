using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    public  class ProductsRepository : Repository<Products>, IProductsRepository
    {
        public ProductsRepository(string connectionString) : base(connectionString)
        {

        }

        public Products GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<Products>().Where(
                products => products.ProductID.Equals(id)).First();
            }
        }

        public bool Insert(Products products)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var result = connection.Execute("insert into Products (ProductName,SupplierId,CategoryID,UnitPrice,Discontinued) " +
                                            "values ( @productName, @supplierId, @categoryID, @unitPrice, @discontinued )",
                                            new
                                            {
                                                productName = products.ProductName,
                                                supplierId = products.SupplierId,
                                                categoryID = products.CategoryID,
                                                unitPrice = products.UnitPrice,
                                                discontinued = products.Discontinued
                                            });

                return Convert.ToBoolean(result);
            }

        }

        public bool Update(Products products)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var result = connection.Execute("update Products " +
                                            "set ProductName = @productName, " +
                                            "UnitPrice = @unitPrice " +
                                            "where ProductID = @productID",
                                            new
                                            {
                                                productName = products.ProductName,
                                                unitPrice = products.UnitPrice,
                                                productID = products.ProductID
                                            });
                /*
                if (result > 0) return true;
                else return false;
                */
                return Convert.ToBoolean(result);
            }

        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute("delete from Products " +
                    "where ProductID = @productID ", new { productID = id });

                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<Products> PagedList()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Products>("dbo.uspProductsPageList",
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TotalProducts> Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var total = connection.Query<TotalProducts>("dbo.uspProductTotal",
                   commandType: System.Data.CommandType.StoredProcedure);
                return total;
            }
        }
    }
}

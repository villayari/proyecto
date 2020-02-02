using System;
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
    public class OrdersRepository : Repository<Orders>, IOrdersRepository
    {
        public OrdersRepository(string connectionString) : base(connectionString)
        {

        }

        public Orders GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<Orders>().Where(
                orders => orders.OrderID.Equals(id)).First();
            }
        }

        public bool Insert(Orders orders)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var result = connection.Execute("insert into Orders (CustomerID,EmployeeID,ShipVia,Freight,ShipName) " +
                                            "values ( @customerID, @employeeID, @shipVia, @freight, @shipName )",
                                            new
                                            {
                                                customerID = orders.CustomerID,
                                                employeeID = orders.EmployeeID,
                                                shipVia = orders.ShipVia,
                                                freight = orders.Freight,
                                                shipName = orders.ShipName
                                            });
                
                return Convert.ToBoolean(result);
            }

        }

        public bool Update(Orders orders)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var result = connection.Execute("update Orders " +
                                            "set ShipVia = @shipVia, " +
                                            "Freight = @freight, " +
                                            "ShipName = @shipName " +
                                            "where OrderID = @orderID",
                                            new
                                            {
                                                shipVia = orders.ShipVia,
                                                freight = orders.Freight,
                                                shipName = orders.ShipName,
                                                orderID = orders.OrderID
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
                var result = connection.Execute("delete from Orders " +
                    "where OrderID = @orderID ", new { orderID = id });

                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<Orders> PagedList()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Orders>("dbo.uspOrdersPageList", 
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TotalOrders> Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var total = connection.Query<TotalOrders>("dbo.upsOrdersTotal",
                   commandType: System.Data.CommandType.StoredProcedure);
                return total;
            }
        }

        public IEnumerable<TotalYears> ReportYears()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var total = connection.Query<TotalYears>("dbo.uspReportYears",
                   commandType: System.Data.CommandType.StoredProcedure);
                return total;
            }
        }

        public IEnumerable<TotalMonth> ReportMonths()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var total = connection.Query<TotalMonth>("dbo.uspReportMonth",
                   commandType: System.Data.CommandType.StoredProcedure);
                return total;
            }
        }

        //public int CountFreight()
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        return connection.ExecuteScalar<int>("Select sum(Freight) from Orders");
        //    }
        //}
    }
}

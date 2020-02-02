using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface IOrdersRepository : IRepository<Orders>
    {
        Orders GetById(int id);
        bool Insert(Orders orders);
        bool Update(Orders orders);
        bool Delete(int id);
        IEnumerable<Orders> PagedList();
        IEnumerable<TotalOrders> Count();
        IEnumerable<TotalYears> ReportYears();
        IEnumerable<TotalMonth> ReportMonths();
        //int CountFreight();
    }
}

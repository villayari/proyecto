using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.NorthWind
{
    public interface IProductsRepository : IRepository<Products>
    {
        Products GetById(int id);
        bool Insert(Products products);
        bool Update(Products products);
        bool Delete(int id);
        IEnumerable<Products> PagedList();
        IEnumerable<TotalProducts> Count();
    }
}

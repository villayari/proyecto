using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.NorthWind
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        
        IEnumerable<OrderDetails> GetListByOrderId(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Repositories.NorthWind;

namespace Cibertec.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomersRepository Customers { get; }
        IOrdersRepository Orders { get; }
        ICabeceraCotizacionRepository CabeceraCotizacion { get; }
        IDetalleCotizacionRepository DetalleCotizacion { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IProductsRepository Products { get; }
        IUserRepository Users { get; }
    }
}

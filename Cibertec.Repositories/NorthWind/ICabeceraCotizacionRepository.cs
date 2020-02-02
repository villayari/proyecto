using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface ICabeceraCotizacionRepository : IRepository<CabeceraCotizacion>
    {
        CabeceraCotizacion GetById(string id);
        bool Update(CabeceraCotizacion cabeceraCotizacion);
        bool Delete(string id);
        IEnumerable<CabeceraCotizacion> PagedList();
        IEnumerable<TotalCotizacion> Count();
        IEnumerable<TotalVentaAsesor> ReportVentaAsesor();
        IEnumerable<TotalMonthCotizacion> ReportMonthCotizacion();
    }
}

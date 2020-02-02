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
    class CabeceraCotizacionRepository : Repository<CabeceraCotizacion>, ICabeceraCotizacionRepository
    {
        public CabeceraCotizacionRepository(string connectionString) : base(connectionString)
        {

        }

        public CabeceraCotizacion GetById(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<CabeceraCotizacion>().Where(
                cotizacionCabecera => cotizacionCabecera.Cotizacion.Equals(id)).First();
            }
        }

        public bool Update(CabeceraCotizacion cabeceraCotizacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var result = connection.Execute("update CabeceraCotizacion " +
                                            "set Evento = @evento, " +
                                            "Asesor = @asesor " +
                                            "where Cotizacion = @cotizacion",
                                            new
                                            {
                                                evento = cabeceraCotizacion.Evento,
                                                asesor = cabeceraCotizacion.Asesor,
                                                cotizacion = cabeceraCotizacion.Cotizacion
                                            });
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
                var result = connection.Execute("delete from CabeceraCotizacion " +
                    "where Cotizacion = @cotizacion ", new { cotizacion = id });

                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<CabeceraCotizacion> PagedList()
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<CabeceraCotizacion>("dbo.uspCotizacionCabeceraPageList",
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TotalCotizacion> Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var total = connection.Query<TotalCotizacion>("dbo.upsCotizacionCabeceraTotal",
                   commandType: System.Data.CommandType.StoredProcedure);
                return total;
            }
        }

        public IEnumerable<TotalVentaAsesor> ReportVentaAsesor()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var total = connection.Query<TotalVentaAsesor>("dbo.uspReportVentaAsesor",
                   commandType: System.Data.CommandType.StoredProcedure);
                return total;
            }
        }

        public IEnumerable<TotalMonthCotizacion> ReportMonthCotizacion()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var total = connection.Query<TotalMonthCotizacion>("dbo.uspReportMonthCotizacion",
                   commandType: System.Data.CommandType.StoredProcedure);
                return total;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
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
    class DetalleCotizacionRepository : Repository<DetalleCotizacion>, IDetalleCotizacionRepository
    {
        public DetalleCotizacionRepository(string connectionString) : base(connectionString)
        {

        }

        public IEnumerable<DetalleCotizacion> GetById(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var queryParametrs = new DynamicParameters();
                queryParametrs.Add("@Cotizacion", id);
                return connection.Query<DetalleCotizacion>("upsDetalleCotizacion",
                    queryParametrs, commandType: CommandType.StoredProcedure);
                
            }
        }
    }
}

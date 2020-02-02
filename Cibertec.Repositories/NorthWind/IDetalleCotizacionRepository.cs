﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface IDetalleCotizacionRepository : IRepository<DetalleCotizacion>
    {
        IEnumerable<DetalleCotizacion> GetById(string id);
    }
}

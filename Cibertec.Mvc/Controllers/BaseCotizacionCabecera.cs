using Cibertec.Mvc.ActionFilters;
using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.Mvc.Controllers
{
    [ErrorActionFilter]
    [Authorize]
    public class BaseCotizacionCabecera : Controller
    {
        protected readonly IUnitOfWork _unit;
        protected readonly ILog _log;

        public BaseCotizacionCabecera(ILog log, IUnitOfWork unit)
        {
            _log = log;
            _unit = unit;
        }
    }
}
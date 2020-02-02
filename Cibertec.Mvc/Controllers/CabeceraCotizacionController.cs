using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cibertec.UnitOfWork;
using System.Configuration;
using log4net;
using Cibertec.Models;

namespace Cibertec.Mvc.Controllers
{
    [RoutePrefix("CabeceraCotizacion")]
    public class CabeceraCotizacionController : BaseCotizacionCabecera
    {
        public CabeceraCotizacionController(ILog log, IUnitOfWork unit) : base(log, unit)
        {

        }

        //Simulación de Error
        public ActionResult Error()
        {
            throw new System.Exception("Prueba de Validación de Error - Action Filter");
        }

        // GET: Orders
        public ActionResult Index()
        {
            _log.Info("Ejecución de Orders Controller Ok");
            return View(_unit.CabeceraCotizacion.GetList());
        }

        //CREATE: Orders
        //public ActionResult Create()
        public PartialViewResult Create()
        {
            //return View();
            return PartialView("_Create", new CabeceraCotizacion());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CabeceraCotizacion cabeceraCotizacion)
        {
            if (ModelState.IsValid)
            {
                _unit.CabeceraCotizacion.Insert(cabeceraCotizacion);
                return RedirectToAction("Index");
            }

            //return View(customer);
            return PartialView("_Create", cabeceraCotizacion);


        }

        //public ActionResult Update(string id)
        public PartialViewResult Update(string id)
        {
            //return View(_unit.Orders.GetById(id));
            return PartialView("_Update", _unit.CabeceraCotizacion.GetById(id));
        }

        public PartialViewResult Detail(string id)
        {
            //return View(_unit.Orders.GetById(id));
            return PartialView("_Detail", _unit.DetalleCotizacion.GetById(id));
        }

        [HttpPost]
        public ActionResult Update(CabeceraCotizacion cabeceraCotizacion)
        {
            var val = _unit.CabeceraCotizacion.Update(cabeceraCotizacion);

            if (val)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Update", cabeceraCotizacion);
            //return View(orders);
        }

        //public ActionResult Delete(String id)
        public PartialViewResult Delete(string id)
        {
            //return View(_unit.Orders.GetById(id));
            return PartialView("_Delete", _unit.CabeceraCotizacion.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(string id)
        {
            var val = _unit.CabeceraCotizacion.Delete(id);

            if (val) return RedirectToAction("Index");

            //return View();
            return View();
        }

        [Route("List/")]
        public PartialViewResult List()
        {
            return PartialView("_List", _unit.CabeceraCotizacion.PagedList());
        }

        [HttpGet]
        [Route("Count/")]
        public JsonResult Count()
        {
            var totalRecords = _unit.CabeceraCotizacion.Count().ToList();
            //return totalRecords.First();
            var response = Json(new
            {
                Error = false,
                Value = totalRecords.First(),
                Message = ""
            }, JsonRequestBehavior.AllowGet);
            return response;
        }

        [HttpGet]
        [Route("ReportVentaAsesor/")]
        public JsonResult ReportVentaAsesor()
        {
            var totalReports = _unit.CabeceraCotizacion.ReportVentaAsesor();
            //return totalRecords.First();
            var response = Json(new
            {
                Error = false,
                Value = totalReports,
                Message = ""
            }, JsonRequestBehavior.AllowGet);
            return response;
        }

        [HttpGet]
        [Route("ReportMonthCotizacion/")]
        public JsonResult ReportMonthCotizacion()
        {
            var totalReports = _unit.CabeceraCotizacion.ReportMonthCotizacion();
            //return totalRecords.First();
            var response = Json(new
            {
                Error = false,
                Value = totalReports,
                Message = ""
            }, JsonRequestBehavior.AllowGet);
            return response;
        }
    }
}
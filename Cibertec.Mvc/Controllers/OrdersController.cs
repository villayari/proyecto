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
    [RoutePrefix("Orders")]
    public class OrdersController : BaseOrders
    {
        //private readonly IUnitOfWork _unit;

        public OrdersController(ILog log, IUnitOfWork unit) : base(log, unit)
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
            return View(_unit.Orders.GetList());
        }

        //CREATE: Orders
        //public ActionResult Create()
        public PartialViewResult Create()
        {
            //return View();
            return PartialView("_Create", new Orders());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Orders orders)
        {
            if (ModelState.IsValid)
            {
                _unit.Orders.Insert(orders);
                return RedirectToAction("Index");
            }

            //return View(customer);
            return PartialView("_Create", orders);


        }

        //public ActionResult Update(string id)
        public PartialViewResult Update(int id)
        {
            //return View(_unit.Orders.GetById(id));
            return PartialView("_Update",_unit.Orders.GetById(id));
        }

        public PartialViewResult Detail(int id)
        {
            //return View(_unit.Orders.GetById(id));
            return PartialView("_Detail", _unit.OrderDetails.GetListByOrderId(id));
        }

        [HttpPost]
        public ActionResult Update(Orders orders)
        {
            var val = _unit.Orders.Update(orders);

            if (val)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Update", orders);
            //return View(orders);
        }

        //public ActionResult Delete(String id)
        public PartialViewResult Delete(int id)
        {
            //return View(_unit.Orders.GetById(id));
            return PartialView("_Delete", _unit.Orders.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var val = _unit.Orders.Delete(id);

            if (val) return RedirectToAction("Index");

            //return View();
            return View();
        }

        [Route("List/")]
        public PartialViewResult List()
        {
            return PartialView("_List", _unit.Orders.PagedList());
        }

        [HttpGet]
        [Route("Count/")]
        public JsonResult Count()
        {
            var totalRecords = _unit.Orders.Count().ToList();
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
        [Route("ReportYears/")]
        public JsonResult ReportYears()
        {
            var totalReports = _unit.Orders.ReportYears();
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
        [Route("ReportMonth/")]
        public JsonResult ReportMonths()
        {
            var totalReports = _unit.Orders.ReportMonths();
            //return totalRecords.First();
            var response = Json(new
            {
                Error = false,
                Value = totalReports,
                Message = ""
            }, JsonRequestBehavior.AllowGet);
            return response;
        }

        //[Route("CountFreight/{rows:int}")]
        //public int CountFreight()
        //{
        //    var totalFreight = _unit.Orders.CountFreight();
        //    return totalFreight;
        //}
    }
}
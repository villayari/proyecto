using Cibertec.Models;
using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.Mvc.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController : BaseProducts
    {
        public ProductsController(ILog log, IUnitOfWork unit) : base(log, unit)
        {
            //_unit = unit;
        }

        //Simulación de Error
        public ActionResult Error()
        {
            throw new System.Exception("Prueba de Validación de Error - Action Filter");
        }

        // GET: Products
        public ActionResult Index()
        {
            _log.Info("Ejecución de Products Controller Ok");
            return View(_unit.Products.GetList());
        }

        //CREATE: Products
        //public ActionResult Create()
        public PartialViewResult Create()
        {
            //return View();
            return PartialView("_Create", new Products());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products)
        {

            if (ModelState.IsValid)
            {
                _unit.Products.Insert(products);
                return RedirectToAction("Index");
            }

            //return View(customer);
            return PartialView("_Create", products);

        }

        //public ActionResult Update(string id)
        public PartialViewResult Update(int id)
        {
            //return View(_unit.Customers.GetById(id));
            return PartialView("_Update", _unit.Products.GetById(id));
        }

        [HttpPost]
        public ActionResult Update(Products products)
        {
            var val = _unit.Products.Update(products);

            if (val)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Update", products);
            //return View(products);
        }

        //public ActionResult Delete(String id)
        public PartialViewResult Delete(int id)
        {
            //return View(_unit.Products.GetById(id));
            return PartialView("_Delete",_unit.Products.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var val = _unit.Products.Delete(id);

            if (val) return RedirectToAction("Index");

            //return View();
            return View();
        }

        [Route("List/")]
        public PartialViewResult List()
        {
            return PartialView("_List", _unit.Products.PagedList());
        }

        [HttpGet]
        [Route("Count/")]
        public JsonResult Count()
        {
            var totalRecords = _unit.Products.Count().ToList();
            //return totalRecords.First();
            var response = Json(new
            {
                Error = false,
                Value = totalRecords.First(),
                Message = ""
            }, JsonRequestBehavior.AllowGet);
            return response;
        }
    }
}
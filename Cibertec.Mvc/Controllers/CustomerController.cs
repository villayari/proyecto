using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cibertec.Repositories.Dapper.NorthWind;
using Cibertec.UnitOfWork;
using System.Configuration;
using Cibertec.Models;
using log4net;
using Cibertec.Mvc.ActionFilters;

namespace Cibertec.Mvc.Controllers
{
    //[ErrorActionFilter]
    [RoutePrefix("Customer")]

    public class CustomerController : BaseController
    {
        /*Ya no es necesario porque se maneja en el padre BaseController*/
        //private readonly IUnitOfWork _unit;

        /*Ya no es necesario porque se utiliza Inyección de Dependencia*/
        /*
        public CustomerController()
        {
            _unit = new NorthwindUnitOfWork(
                ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        }
        */
        public CustomerController(ILog log, IUnitOfWork unit): base(log,unit)
        {
            //_unit = unit;
        }

        //Simulación de Error
        public ActionResult Error()
        {
            throw new System.Exception("Prueba de Validación de Error - Action Filter");
        }

        // GET: Customer
        public ActionResult Index()
        {
            _log.Info("Ejecución de Customer Controller Ok");
            return View(_unit.Customers.GetList());
        }

        //CREATE: Customer
        //public ActionResult Create()
        public PartialViewResult Create()
        {
            //return View();
            return PartialView("_Create", new Customers());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customers customer)
        {
            
            if (ModelState.IsValid)
            {
                _unit.Customers.Insert(customer);
                return RedirectToAction("Index");
            }

            //return View(customer);
            return PartialView("_Create", customer);
            
        }

        //public ActionResult Update(string id)
        public PartialViewResult Update(string id)
        {
            //return View(_unit.Customers.GetById(id));
            return PartialView("_Update",_unit.Customers.GetById(id));
        }

        [HttpPost]
        public ActionResult Update(Customers customer)
        {
            var val = _unit.Customers.Update(customer);

            if (val)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Update", customer);
            //return View(customer);
        }

        //public ActionResult Delete(String id)
        public PartialViewResult Delete(string id)
        {
            //return View(_unit.Customers.GetById(id));
            return PartialView("_Delete",_unit.Customers.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(string id)
        {
            var val = _unit.Customers.Delete(id);

            if (val) return RedirectToAction("Index");

            return View();
        }

        [Route("List/")]
        public PartialViewResult List()
        {
            return PartialView("_List", _unit.Customers.PagedList());
        }   

        [Route("CountPages/{rows:int}")]
        public int CountPages(int rows)
        {
            var totalRecords = _unit.Customers.Count();
            return totalRecords % rows != 0 ? (totalRecords / rows) + 1 : totalRecords / rows;
        }
    }
}
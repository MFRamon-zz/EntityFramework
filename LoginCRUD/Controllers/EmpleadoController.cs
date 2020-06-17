using LoginCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginCRUD.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            using (CRUDEmpleadosEntities1 dbModel = new CRUDEmpleadosEntities1())
            return View(dbModel.Empleadoes.ToList());
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            Empleado empleado = new Empleado();
            return View(empleado);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Empleado empleado)
        {
            using (CRUDEmpleadosEntities1 dbModel = new CRUDEmpleadosEntities1())
            {
                if (dbModel.Empleadoes.Any(x => x.Username == empleado.Username))
                {
                    ViewBag.DuplicateMessage = "El usuario ya existe";
                    return View("AddOrEdit", empleado);
                }
                dbModel.Empleadoes.Add(empleado);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registro exitoso";
            return View("AddOrEdit", new Empleado());
        }

        public ActionResult Delete(int id)
        {
            using (CRUDEmpleadosEntities1 dbModel = new CRUDEmpleadosEntities1())
            {
                return View(dbModel.Empleadoes.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (CRUDEmpleadosEntities1 dbModel = new CRUDEmpleadosEntities1())
                {
                    Empleado empleado = dbModel.Empleadoes.Where(XmlSiteMapProvider => XmlSiteMapProvider.Id == id).FirstOrDefault();
                    dbModel.Empleadoes.Remove(empleado);
                    dbModel.SaveChanges();
                }

                return RedirectToAction("Index");
            } catch
            {
                return View();
            }
        }
    }
}
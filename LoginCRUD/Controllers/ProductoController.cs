using LoginCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginCRUD.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (DBModelProducto dbModel = new DBModelProducto())
            return View(dbModel.Productoes.ToList());
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            Producto producto = new Producto();
            return View(producto);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Producto producto)
        {
            using (DBModelProducto dbModel = new DBModelProducto())
            {
                if (dbModel.Productoes.Any(x => x.Nombre == producto.Nombre))
                {
                    ViewBag.DuplicateMessage = "El producto ya existe";
                    return View("AddOrEdit", producto);
                }
                dbModel.Productoes.Add(producto);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registro exitoso";
            return View("AddOrEdit", new Producto());
        }

        public ActionResult Delete(int id)
        {
            using (DBModelProducto dbModel = new DBModelProducto())
            {
                return View(dbModel.Productoes.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DBModelProducto dbModel = new DBModelProducto())
                {
                    Producto producto = dbModel.Productoes.Where(XmlSiteMapProvider => XmlSiteMapProvider.Id == id).FirstOrDefault();
                    dbModel.Productoes.Remove(producto);
                    dbModel.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
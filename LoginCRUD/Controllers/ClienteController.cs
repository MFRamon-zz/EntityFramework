using LoginCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginCRUD.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (DBCliente dbModel = new DBCliente())
            return View(dbModel.Clientes.ToList());
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            Cliente cliente = new Cliente();
            return View(cliente);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Cliente cliente)
        {
            using (DBCliente dbModel = new DBCliente())
            {
                if (dbModel.Clientes.Any(x => x.Nombre == cliente.Nombre))
                {
                    ViewBag.DuplicateMessage = "El cliente ya existe";
                    return View("AddOrEdit", cliente);
                }
                dbModel.Clientes.Add(cliente);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registro exitoso";
            return View("AddOrEdit", new Cliente());
        }

        public ActionResult Delete(int id)
        {
            using (DBCliente dbModel = new DBCliente())
            {
                return View(dbModel.Clientes.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DBCliente dbModel = new DBCliente())
                {
                    Cliente cliente = dbModel.Clientes.Where(XmlSiteMapProvider => XmlSiteMapProvider.Id == id).FirstOrDefault();
                    dbModel.Clientes.Remove(cliente);
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
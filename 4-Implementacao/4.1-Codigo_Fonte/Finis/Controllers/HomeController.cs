using Finis.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finis.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")]
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult TotalClientes()
        {
            int totClientes = db.Cliente.Count();

            var obj = new
            {
                Total = totClientes.ToString()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
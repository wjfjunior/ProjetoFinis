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
        public ActionResult Index()
        {
            return View();
        }
    }
}
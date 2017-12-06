using Finis.DAL;
using Finis.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finis.Controllers
{
    public class ItensController : Controller
    {
        private Contexto db = new Contexto();
        ICollection<Item> itens;

        public ActionResult ListarItens()
        {
            itens = new Collection<Item>();

            return PartialView(itens);
        }

        public ActionResult AdicionarItem(int  id)
        {
            var item = db.Item.Find(id);
            itens.Add(item);
            
            return PartialView(itens);
        }

        [HttpPost]
        public JsonResult DropboxItens (string item)
        {
            var listaItens = db.Item.Where(i => i.nome.Contains(item) || i.id.Equals(item)).ToList();
            
            return Json(listaItens, "text/html", JsonRequestBehavior.AllowGet);
        }

    }
}
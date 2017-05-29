using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Finis.DAL;
using Finis.Models;

namespace Finis.Controllers
{
    public class EditorasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Editoras
        public ActionResult Index()
        {
            return View(db.Editora.ToList());
        }

        // GET: Editoras/Create
        public ActionResult Form()
        {
            ViewBag.Title = "Nova Editora";
            return View();
        }

        // POST: Editoras/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form(Editora editora)
        {
            if (ModelState.IsValid)
            {
                if(editora.id == 0)
                {
                    db.Editora.Add(editora);
                }
                else
                {
                    db.Entry(editora).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editora);
        }

        // GET: Editoras/Edit/5
        public ActionResult Form(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editora editora = db.Editora.Find(id);
            if (editora == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Editar Editora";
            return View(editora);
        }

        // GET: Clientes/Details/5
        public JsonResult Detalhes(int? id)
        {
            bool sucesso;
            string resultado;

            if (id == null)
            {
                sucesso = false;
                resultado = "Não encontrado!";
            }
            else
            {
                Editora editora = db.Editora.Find(id);
                if (editora == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = editora.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Editora editora = db.Editora.Find(id);
                db.Editora.Remove(editora);
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

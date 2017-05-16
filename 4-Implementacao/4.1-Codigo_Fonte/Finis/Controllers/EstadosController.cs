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
    public class EstadosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Estados
        public ActionResult Index()
        {
            return View(db.Estado.ToList());
        }

        // GET: Estados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.Estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
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
                Estado estado = db.Estado.Find(id);
                if (estado == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = estado.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Estados/Create
        public ActionResult Create()
        {
            ViewBag.Paises = new SelectList(db.Pais, "Id", "Nome", "Sigla");
            return View();
        }

        // POST: Estados/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estado model)
        {
            if (ModelState.IsValid)
            {
                db.Estado.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Paises = new SelectList(db.Pais, "Id", "Nome", "Sigla");
            return View(model);
        }

        // GET: Estados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.Estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Paises = new SelectList(db.Pais, "Id", "Nome", "Sigla");
            return View(estado);
        }

        // POST: Estados/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Estado model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Paises = new SelectList(db.Pais, "Id", "Nome", "Sigla");
            return View(model);
        }

        // GET: Estados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.Estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estado estado = db.Estado.Find(id);
            db.Estado.Remove(estado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Estado estado = db.Estado.Find(id);
                db.Estado.Remove(estado);
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

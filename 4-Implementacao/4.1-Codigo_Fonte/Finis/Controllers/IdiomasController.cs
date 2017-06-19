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
    public class IdiomasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Idiomas
        public ActionResult Index()
        {
            var idiomas = db.Idiomas.Include(i => i.pais);
            return View(idiomas.ToList());
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
                Idioma idioma = db.Idiomas.Find(id);
                if (idioma == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = idioma.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Idiomas/Create
        public ActionResult Create()
        {
            ViewBag.paisId = new SelectList(db.Pais, "id", "nome");
            return View();
        }

        // POST: Idiomas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,paisId,user_insert,user_update,date_insert,date_update")] Idioma idioma)
        {
            if (ModelState.IsValid)
            {
                db.Idiomas.Add(idioma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.paisId = new SelectList(db.Pais, "id", "nome", idioma.paisId);
            return View(idioma);
        }

        // GET: Idiomas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idioma idioma = db.Idiomas.Find(id);
            if (idioma == null)
            {
                return HttpNotFound();
            }
            ViewBag.paisId = new SelectList(db.Pais, "id", "nome", idioma.paisId);
            return View(idioma);
        }

        // POST: Idiomas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,paisId,user_insert,user_update,date_insert,date_update")] Idioma idioma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(idioma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.paisId = new SelectList(db.Pais, "id", "nome", idioma.paisId);
            return View(idioma);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Idioma idioma = db.Idiomas.Find(id);
                db.Idiomas.Remove(idioma);
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

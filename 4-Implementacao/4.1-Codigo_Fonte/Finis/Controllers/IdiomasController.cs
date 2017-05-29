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
            var idiomas = db.Idioma.Include(i => i.pais);
            return View(idiomas.ToList());
        }

        // GET: Idiomas/Create
        public ActionResult Form()
        {
            ViewBag.paisId = new SelectList(db.Pais, "id", "nome");
            ViewBag.Title = "Cadastro de Idiomas";
            return View();
        }

        // POST: Idiomas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form([Bind(Include = "id,nome,paisId,user_insert,user_update,date_insert,date_update")] Idioma idioma)
        {
            if (ModelState.IsValid)
            {
                if(idioma.id == 0)
                {
                    db.Idioma.Add(idioma);
                }
                else
                {
                    db.Entry(idioma).State = EntityState.Modified;
                }
                db.SaveChanges();
                ViewBag.Title = "Cadastro de Idiomas";
                return RedirectToAction("Index");
            }

            ViewBag.paisId = new SelectList(db.Pais, "id", "nome", idioma.paisId);
            return View(idioma);
        }

        // GET: Idiomas/Edit/5
        public ActionResult Form(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idioma idioma = db.Idioma.Find(id);
            if (idioma == null)
            {
                return HttpNotFound();
            }
            ViewBag.paisId = new SelectList(db.Pais, "id", "nome", idioma.paisId);
            ViewBag.Title = "Cadastro de Idiomas";
            return View(idioma);
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
                Idioma idioma = db.Idioma.Find(id);
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

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Idioma idioma = db.Idioma.Find(id);
                db.Idioma.Remove(idioma);
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

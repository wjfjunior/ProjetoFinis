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
    public class SessoesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Sessoes
        public ActionResult Index()
        {
            return View(db.Sessoes.ToList());
        }

        // GET: Sessoes/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Nova Sessão";
            return View();
        }

        // POST: Sessoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,user_insert,user_update,date_insert,date_update")] Sessao sessao)
        {
            if (ModelState.IsValid)
            {
                db.Sessoes.Add(sessao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(sessao);
        }

        // GET: Sessoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessao sessao = db.Sessoes.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Editar Sessão";
            return View(sessao);
        }

        // POST: Sessoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,user_insert,user_update,date_insert,date_update")] Sessao sessao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sessao);
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
                Sessao sessao = db.Sessoes.Find(id);
                if (sessao == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = sessao.Serializar();
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
                Sessao sessao = db.Sessoes.Find(id);
                db.Sessoes.Remove(sessao);
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

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
            return View(db.Sessao.ToList());
        }

        // GET: Sessoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessao sessao = db.Sessao.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
            return View(sessao);
        }

        // GET: Sessoes/Create
        public ActionResult Create()
        {
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
                db.Sessao.Add(sessao);
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
            Sessao sessao = db.Sessao.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
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

        // GET: Sessoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessao sessao = db.Sessao.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
            return View(sessao);
        }

        // POST: Sessoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sessao sessao = db.Sessao.Find(id);
            db.Sessao.Remove(sessao);
            db.SaveChanges();
            return RedirectToAction("Index");
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

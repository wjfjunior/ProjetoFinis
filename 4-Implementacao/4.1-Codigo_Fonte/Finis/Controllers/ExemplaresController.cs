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
    public class ExemplaresController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Exemplares
        public ActionResult Index()
        {
            var exemplars = db.Exemplar.Include(e => e.editora).Include(e => e.idioma).Include(e => e.sessao);
            ViewBag.Title = "Cadastro de Exemplares";
            return View(exemplars.ToList());
        }

        // GET: Exemplares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = db.Exemplar.Find(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            return View(exemplar);
        }

        // GET: Exemplares/Create
        public ActionResult Form()
        {
            ViewBag.editoraId = new SelectList(db.Fornecedor, "id", "cnpj");
            ViewBag.idiomaId = new SelectList(db.Idiomas, "id", "nome");
            ViewBag.sessaoId = new SelectList(db.Sessaos, "id", "nome");
            ViewBag.Title = "Inserir Exemplar";
            return View();
        }

        // POST: Exemplares/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form([Bind(Include = "id,titulo,conservacao,isbn,ano,edicao,preco,descricao,peso,vendaOnline,quantidade,editoraId,idiomaId,sessaoId,user_insert,user_update,date_insert,date_update")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Exemplar.Add(exemplar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.editoraId = new SelectList(db.Fornecedor, "id", "cnpj", exemplar.editoraId);
            ViewBag.idiomaId = new SelectList(db.Idiomas, "id", "nome", exemplar.idiomaId);
            ViewBag.sessaoId = new SelectList(db.Sessaos, "id", "nome", exemplar.sessaoId);
            return View(exemplar);
        }

        // GET: Exemplares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = db.Exemplar.Find(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            ViewBag.editoraId = new SelectList(db.Fornecedor, "id", "cnpj", exemplar.editoraId);
            ViewBag.idiomaId = new SelectList(db.Idiomas, "id", "nome", exemplar.idiomaId);
            ViewBag.sessaoId = new SelectList(db.Sessaos, "id", "nome", exemplar.sessaoId);
            ViewBag.Title = "Editar Exemplar";
            return View("Form", exemplar);
        }

        // POST: Exemplares/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titulo,conservacao,isbn,ano,edicao,preco,descricao,peso,vendaOnline,quantidade,editoraId,idiomaId,sessaoId,user_insert,user_update,date_insert,date_update")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exemplar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.editoraId = new SelectList(db.Fornecedor, "id", "cnpj", exemplar.editoraId);
            ViewBag.idiomaId = new SelectList(db.Idiomas, "id", "nome", exemplar.idiomaId);
            ViewBag.sessaoId = new SelectList(db.Sessaos, "id", "nome", exemplar.sessaoId);
            return View("Form", exemplar);
        }

        // GET: Exemplares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = db.Exemplar.Find(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            return View(exemplar);
        }

        // POST: Exemplares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exemplar exemplar = db.Exemplar.Find(id);
            db.Exemplar.Remove(exemplar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Exemplar exemplar = db.Exemplar.Find(id);
                //Endereco endereco = this.RecuperaEndereco(cliente.enderecoId);
                db.Exemplar.Remove(exemplar);
                //db.Endereco.Remove(endereco);
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
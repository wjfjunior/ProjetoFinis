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
            var exemplares = db.Exemplar.Include(e => e.editora).Include(e => e.idioma).Include(e => e.sessao);
            return View(exemplares.ToList());
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
                Exemplar exemplar = db.Exemplar.Find(id);
                if (exemplar == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    exemplar.editora = this.RecuperaEditora(exemplar.editoraId);
                    exemplar.idioma = this.RecuperaIdioma(exemplar.idiomaId);
                    exemplar.sessao = this.RecuperaSessao(exemplar.sessaoId);

                    sucesso = true;
                    resultado = exemplar.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        private Editora RecuperaEditora(int? id)
        {
            if (id != null)
            {
                Editora editora = db.Fornecedors.Find(id);
                return editora;
            }
            return new Editora();
        }

        private Idioma RecuperaIdioma(int? id)
        {
            if (id != null)
            {
                Idioma idioma = db.Idiomas.Find(id);
                return idioma;
            }
            return new Idioma();
        }

        private Sessao RecuperaSessao(int? id)
        {
            if (id != null)
            {
                Sessao sessao = db.Sessaos.Find(id);
                return sessao;
            }
            return new Sessao();
        }

        // GET: Exemplares/Create
        public ActionResult Create()
        {
            ViewBag.editoraId = new SelectList(db.Fornecedor.Where(f => f.tipoFornecedor == TipoFornecedor.EDITORA), "id", "nome");
            ViewBag.idiomaId = new SelectList(db.Idiomas, "id", "nome");
            ViewBag.sessaoId = new SelectList(db.Sessaos, "id", "nome");
            return View();
        }

        // POST: Exemplares/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titulo,conservacao,isbn,ano,edicao,precoCompra,precoVenda,descricao,peso,vendaOnline,quantidade,editoraId,idiomaId,sessaoId,user_insert,user_update,date_insert,date_update")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Exemplar.Add(exemplar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.editoraId = new SelectList(db.Fornecedor.Where(f => f.tipoFornecedor == TipoFornecedor.EDITORA), "id", "nome");
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
            ViewBag.Editoras = new SelectList(db.Fornecedor.Where(f => f.tipoFornecedor == TipoFornecedor.EDITORA), "id", "nome", "Editora");
            ViewBag.Idiomas = new SelectList(db.Idiomas, "id", "nome", exemplar.idiomaId);
            ViewBag.Sessoes = new SelectList(db.Sessaos, "id", "nome", exemplar.sessaoId);

            exemplar.editora = this.RecuperaEditora(exemplar.editoraId);
            exemplar.idioma = this.RecuperaIdioma(exemplar.idiomaId);
            exemplar.sessao = this.RecuperaSessao(exemplar.sessaoId);

            return View(exemplar);
        }

        // POST: Exemplares/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titulo,conservacao,isbn,ano,edicao,precoCompra,precoVenda,descricao,peso,vendaOnline,quantidade,editoraId,idiomaId,sessaoId,user_insert,user_update,date_insert,date_update")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exemplar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.editoraId = new SelectList(db.Fornecedor.Where(f => f.tipoFornecedor == TipoFornecedor.EDITORA), "id", "nome");
            ViewBag.idiomaId = new SelectList(db.Idiomas, "id", "nome", exemplar.idiomaId);
            ViewBag.sessaoId = new SelectList(db.Sessaos, "id", "nome", exemplar.sessaoId);
            return View(exemplar);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Exemplar exemplar = db.Exemplar.Find(id);
                db.Exemplar.Remove(exemplar);
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

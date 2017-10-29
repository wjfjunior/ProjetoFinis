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
        [Authorize(Roles = "Administrador, Funcionário")]
        public ActionResult Index()
        {
            var exemplares = db.Exemplar.Include(e => e.editora).Include(e => e.idioma).Include(e => e.sessao).OrderBy(e => e.titulo);
            return View(exemplares.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Funcionário")]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Exemplar
                .Include(e => e.editora)
                .Include(e => e.idioma)
                .Include(e => e.sessao)
                .Where(c => c.titulo.Contains(pesquisar) || c.isbn.Contains(pesquisar))
                .OrderBy(e => e.titulo)
                .ToList());
        }

        // GET: Exemplares
        [Authorize(Roles = "Administrador, Funcionário, Cliente")]
        public ActionResult Consulta()
        {
            var exemplares = db.Exemplar.Include(e => e.editora).Include(e => e.idioma).Include(e => e.sessao).OrderBy(e => e.titulo);
            return View(exemplares.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Funcionário, Cliente")]
        public ActionResult Consulta(string pesquisar)
        {
            return View(db.Exemplar
                .Include(e => e.editora)
                .Include(e => e.idioma)
                .Include(e => e.sessao)
                .Where(c => c.titulo.Contains(pesquisar) || c.isbn.Contains(pesquisar))
                .OrderBy(e => e.titulo)
                .ToList());
        }

        [HttpGet]
        public JsonResult DropboxEditoras()
        {
            var listaEditora = db.Fornecedors
                .Where(e => e.tipoFornecedor == TipoFornecedor.EDITORA)
                .Select(p => new { p.id, p.nome })
                .OrderBy(p => p.nome)
                .ToArray();

            var obj = new
            {
                lista = listaEditora
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraNomeEditora(Exemplar exemplar)
        {
            if (exemplar.editora == null)
            {
                exemplar.editora = db.Fornecedors.Find(exemplar.editoraId);
            }
            exemplar.editoraNome = exemplar.editora.id + " - " + exemplar.editora.nome;
        }

        private bool VerificaJaExiste(Exemplar exemplar)
        {
            List<Exemplar> resultado = new List<Exemplar>();

            resultado = db.Exemplar.Where(e => e.isbn == exemplar.isbn).ToList();
            if (resultado.Count() > 0)
            {
                return true;
            }
            
            return false;
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
        [Authorize(Roles = "Administrador, Funcionário")]
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
        [Authorize(Roles = "Administrador, Funcionário")]
        public ActionResult Create(Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                if (VerificaJaExiste(exemplar))
                {
                    ViewBag.Erro = "Ja existe um registro com o ISBN informado!";
                    return View(exemplar);
                }
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
        [Authorize(Roles = "Administrador, Funcionário")]
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

            this.ConfiguraNomeEditora(exemplar);

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
        [Authorize(Roles = "Administrador, Funcionário")]
        public ActionResult Edit(Exemplar exemplar)
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

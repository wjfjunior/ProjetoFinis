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
using Newtonsoft.Json;

namespace Finis.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")]
    public class CidadesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Cidades
        public ActionResult Index()
        {
            return View(db.Cidade.OrderBy(c => c.nome).ToList());
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Cidade.Where(c => c.nome.Contains(pesquisar)).ToList());
        }

        [HttpGet]
        public JsonResult DropboxEstados()
        {
            var listaEstados = db.Estado.Select(e => new { e.id, e.nome, e.pais.sigla }).OrderBy(e => e.nome).ToArray();

            var obj = new
            {
                lista = listaEstados
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraNomeEstado(Cidade cidade)
        {
            if (cidade.estado == null)
            {
                Estado estado = new Estado();
                estado.id = cidade.estadoId;
                cidade.estado = db.Estado.Find(estado);

                if(cidade.estado.pais == null)
                {
                    Pais pais = new Pais();
                    pais.id = cidade.estado.paisId;
                    cidade.estado.pais = db.Pais.Find(pais);
                }
            }
            cidade.estadoNome = cidade.estado.id + " - " + cidade.estado.nome + "/" + cidade.estado.pais.sigla;
        }

        private bool VerificaJaExiste(Cidade cidade)
        {
            List<Cidade> resultado = new List<Cidade>();

            resultado = db.Cidade.Where(e => e.nome == cidade.nome && e.estadoId == cidade.estadoId).ToList();
            if (resultado.Count() > 0)
            {
                return true;
            }

            return false;
        }

        // GET: Cidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidade.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
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
                Cidade cidade = db.Cidade.Find(id);
                if (cidade == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    if(cidade.estado == null)
                    {
                        cidade.estado = db.Estado.Find(cidade.estadoId);
                    }
                    sucesso = true;
                    resultado = JsonConvert.SerializeObject(cidade);
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Cidades/Create
        public ActionResult Create()
        {
            ViewBag.Estados = new SelectList(db.Estado, "Id", "Nome", "Sigla");
            return View();
        }

        // POST: Cidades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cidade model)
        {
            if (ModelState.IsValid)
            {
                model.nome = model.nome.ToUpper();
                if (VerificaJaExiste(model))
                {
                    ViewBag.Erro = "Ja existe um registro com os valores informados!";
                    return View(model);
                }
                db.Cidade.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estados = new SelectList(db.Estado, "Id", "Nome", "Sigla");
            return View(model);
        }

        // GET: Cidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidade.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estados = new SelectList(db.Estado, "Id", "Nome", "Sigla");
            this.ConfiguraNomeEstado(cidade);
            return View(cidade);
        }

        // POST: Cidades/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cidade model)
        {
            if (ModelState.IsValid)
            {
                model.nome = model.nome.ToUpper();
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estados = new SelectList(db.Estado, "Id", "Nome", "Sigla");
            return View(model);
        }

        // GET: Cidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidade.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estados = new SelectList(db.Estado, "Id", "Nome", "Sigla");
            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cidade cidade = db.Cidade.Find(id);
            db.Cidade.Remove(cidade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Cidade cidade = db.Cidade.Find(id);
                db.Cidade.Remove(cidade);
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

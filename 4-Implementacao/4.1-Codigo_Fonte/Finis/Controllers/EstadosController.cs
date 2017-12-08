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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Finis.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")]
    public class EstadosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Estados
        public ActionResult Index()
        {
            return View(db.Estado.Include(p => p.pais).OrderBy(e => e.nome).ToList());
        }

        public ActionResult Exportar()
        {
            List<Estado> estado = new List<Estado>();
            estado = db.Estado.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Estados.rpt"));
            rd.SetDataSource(estado);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Estados.pdf");
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Estado.Include(p => p.pais).Where(c => c.nome.Contains(pesquisar)).ToList());
        }

        [HttpGet]
        public JsonResult DropboxPaises()
        {
            var listaPaises = db.Pais.Select(p => new {p.id, p.nome}).OrderBy(p => p.nome).ToArray();

            var obj = new
            {
                lista_paises = listaPaises
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraNomePais(Estado estado)
        {
            if(estado.pais == null)
            {
                Pais pais = new Pais();
                pais.id = estado.paisId;
                estado.pais = db.Pais.Find(pais.id);
            }
            estado.paisNome = estado.pais.id + " - " + estado.pais.nome;
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
                    if (estado.pais == null)
                    {
                        estado.pais = db.Pais.Find(estado.paisId);
                    }
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

        private bool VerificaJaExiste(Estado estado)
        {
            List<Estado> resultado = new List<Estado>();

            resultado = db.Estado.Where(e => e.nome == estado.nome && e.paisId == estado.paisId).ToList();
            if(resultado.Count() > 0)
            {
                return true;
            }

            resultado = db.Estado.Where(e => e.sigla == estado.sigla && e.paisId == estado.paisId).ToList();
            if (resultado.Count() > 0)
            {
                return true;
            }

            return false;
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
                model.nome = model.nome.ToUpper();
                model.sigla = model.sigla.ToUpper();
                if (VerificaJaExiste(model))
                {
                    ViewBag.Erro = "Ja existe um registro com os valores informados!";
                    return View(model);
                }
                model.ConfigurarParaSalvar();
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
            this.ConfiguraNomePais(estado);
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
                model.nome = model.nome.ToUpper();
                model.sigla = model.sigla.ToUpper();
                model.ConfigurarParaSalvar();
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

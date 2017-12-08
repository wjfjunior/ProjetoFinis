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
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Finis.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")]
    public class PaisesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Paises
        public ActionResult Index()
        {
            return View(db.Pais.OrderBy(p => p.nome).ToList());
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Pais.Where(c => c.nome.Contains(pesquisar)).ToList());
        }

        public ActionResult Exportar()
        {
            List<Pais> pais = new List<Pais>();
            pais = db.Pais.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Paises.rpt"));
            rd.SetDataSource(pais);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Paises.pdf");
        }

        // GET: Paises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
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
                Pais pais = db.Pais.Find(id);
                if (pais == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = pais.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Paises/Create
        public ActionResult Create()
        {
            return View();
        }

        private bool VerificaJaExiste(Pais pais)
        {
            List<Pais> resultado = new List<Pais>();

            resultado = db.Pais.Where(p => p.nome == pais.nome).ToList();
            if (resultado.Count() > 0)
            {
                return true;
            }

            return false;
        }

        // POST: Paises/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pais model)
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
                db.Pais.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Paises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Paises/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pais pais)
        {
            if (ModelState.IsValid)
            {
                pais.nome = pais.nome.ToUpper();
                pais.sigla = pais.sigla.ToUpper();
                pais.ConfigurarParaSalvar();
                db.Entry(pais).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        // GET: Paises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pais pais = db.Pais.Find(id);
            db.Pais.Remove(pais);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Pais pais = db.Pais.Find(id);
                db.Pais.Remove(pais);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException != null)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
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

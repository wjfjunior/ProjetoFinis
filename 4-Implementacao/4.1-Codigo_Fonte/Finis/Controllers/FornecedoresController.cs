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
    public class FornecedoresController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Fornecedores
        public ActionResult Index()
        {
            return View(db.Fornecedor.OfType<Fornecedor>().OrderBy(f => f.nome).ToList());
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Fornecedor.OfType<Fornecedor>()
                .Where(f => f.nome.Contains(pesquisar))
                .OrderBy(f => f.nome)
                .ToList());
        }

        public ActionResult Exportar()
        {
            List<Fornecedor> fornecedor = new List<Fornecedor>();
            fornecedor = db.Fornecedor.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Fornecedor.rpt"));
            rd.SetDataSource(fornecedor);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Fornecedor.pdf");
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
                Fornecedor model = db.Fornecedor.Find(id);
                if (model == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = model.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Editoras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editoras/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fornecedor model)
        {
            if (ModelState.IsValid)
            {
                model.ConfigurarParaSalvar();
                db.Fornecedor.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Editoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor model = db.Fornecedor.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Editoras/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fornecedor model)
        {
            if (ModelState.IsValid)
            {
                model.ConfigurarParaSalvar();
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Fornecedor model = db.Fornecedor.Find(id);
                db.Fornecedor.Remove(model);
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

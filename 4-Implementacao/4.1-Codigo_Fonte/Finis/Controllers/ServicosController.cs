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
    public class ServicosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Servicos
        public ActionResult Index()
        {
            return View(db.Item.OfType<Servico>().ToList().OrderBy(s => s.nome));
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Item.OfType<Servico>().Where(p => p.nome.Contains(pesquisar)).ToList().OrderBy(p => p.nome));
        }

        // GET: Servicos/Details/5
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
                Servico model = (Servico)db.Item.Find(id);
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

        public ActionResult Exportar()
        {
            List<Servico> servico = new List<Servico>();
            servico = db.Item.OfType<Servico>().ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Servicos.rpt"));
            rd.SetDataSource(servico);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Servicos.pdf");
        }

        private bool VerificaJaExiste(Servico usuario)
        {
            List<Servico> resultado = new List<Servico>();

            resultado = db.Item.OfType<Servico>().Where(e => e.nome.Equals(usuario.nome)).ToList();
            if (resultado.Count() > 0)
            {
                return true;
            }

            return false;
        }

        // GET: Servicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.nome = servico.nome.ToUpper();
                if (VerificaJaExiste(servico))
                {
                    ViewBag.Erro = "Já existe um registro com o nome informado!";
                    return View(servico);
                }
                servico.ConfigurarParaSalvar();
                db.Item.Add(servico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Servicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = (Servico)db.Item.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servicos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.ConfigurarParaSalvar();
                db.Entry(servico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Usuarios/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Servico servico = (Servico)db.Item.Find(id);
                db.Item.Remove(servico);
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

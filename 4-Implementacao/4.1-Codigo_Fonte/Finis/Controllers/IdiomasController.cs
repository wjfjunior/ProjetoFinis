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
    public class IdiomasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Idiomas
        public ActionResult Index()
        {
            var idiomas = db.Idioma.Include(i => i.pais);
            return View(idiomas.ToList().OrderBy(i => i.nome));
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Idioma.Include(i => i.pais).Where(c => c.nome.Contains(pesquisar)).ToList().OrderBy(i => i.nome));
        }

        [HttpGet]
        public JsonResult DropboxPaises()
        {
            var listaPaises = db.Pais.Select(p => new { p.id, p.nome }).OrderBy(p => p.nome).ToArray();

            var obj = new
            {
                lista_paises = listaPaises
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraNomePais(Idioma idioma)
        {
            if (idioma.pais == null)
            {
                idioma.pais = db.Pais.Find(idioma.paisId);
            }
            idioma.paisNome = idioma.pais.id + " - " + idioma.pais.nome;
        }

        private bool VerificaJaExiste(Idioma idioma)
        {
            List<Idioma> resultado = new List<Idioma>();

            resultado = db.Idioma.Where(e => e.nome == idioma.nome && e.paisId == idioma.paisId).ToList();
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
                Idioma idioma = db.Idioma.Find(id);
                if (idioma == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    if (idioma.pais == null)
                    {
                        idioma.pais = db.Pais.Find(idioma.paisId);
                    }
                    sucesso = true;
                    resultado = idioma.Serializar();
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
            List<Idioma> idioma = new List<Idioma>();
            idioma = db.Idioma.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Idiomas.rpt"));
            rd.SetDataSource(idioma);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Idiomas.pdf");
        }

        // GET: Idiomas/Create
        public ActionResult Create()
        {
            ViewBag.paisId = new SelectList(db.Pais, "id", "nome");
            return View();
        }

        // POST: Idiomas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Idioma idioma)
        {
            if (ModelState.IsValid)
            {
                idioma.nome = idioma.nome.ToUpper();
                if (VerificaJaExiste(idioma))
                {
                    ViewBag.Erro = "Ja existe um registro com os valores informados!";
                    return View(idioma);
                }
                idioma.ConfigurarParaSalvar();
                db.Idioma.Add(idioma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.paisId = new SelectList(db.Pais, "id", "nome", idioma.paisId);
            return View(idioma);
        }

        // GET: Idiomas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idioma idioma = db.Idioma.Find(id);
            if (idioma == null)
            {
                return HttpNotFound();
            }
            ViewBag.paisId = new SelectList(db.Pais, "id", "nome", idioma.paisId);
            this.ConfiguraNomePais(idioma);
            return View(idioma);
        }

        // POST: Idiomas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Idioma idioma)
        {
            if (ModelState.IsValid)
            {
                idioma.nome = idioma.nome.ToUpper();
                idioma.ConfigurarParaSalvar();
                db.Entry(idioma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.paisId = new SelectList(db.Pais, "id", "nome", idioma.paisId);
            return View(idioma);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Idioma idioma = db.Idioma.Find(id);
                db.Idioma.Remove(idioma);
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

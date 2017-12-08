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
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace Finis.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")]
    public class ProdutosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Produtos
        public ActionResult Index()
        {
            var produto = db.Item.OfType<Produto>().Include(p => p.unidadeMedida).Include(m => m.marca);
            return View(produto.ToList().OrderBy(p => p.nome));
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Item.OfType<Produto>().Include(p => p.unidadeMedida).Include(m => m.marca).Where(p => p.nome.Contains(pesquisar)).ToList().OrderBy(p => p.nome));
        }

        [HttpGet]
        public JsonResult DropboxUnidadesMedida()
        {
            var listaUnidadesMedida = db.UnidadeMedida.Select(p => new { p.id, p.unidade, p.simbolo }).OrderBy(p => p.unidade).ToArray();

            var obj = new
            {
                lista = listaUnidadesMedida
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Exportar()
        {
            List<Produto> produto = new List<Produto>();
            produto = db.Item.OfType<Produto>().ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Produtos.rpt"));
            rd.SetDataSource(produto);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Produtos.pdf");
        }

        [HttpGet]
        public JsonResult DropboxMarca()
        {
            var listaMarcas = db.Marca.Select(p => new { p.id, p.nome }).OrderBy(p => p.nome).ToArray();

            var obj = new
            {
                lista = listaMarcas
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraNomeMarca(Produto produto)
        {
            if (produto.marca == null)
            {
                produto.marca = db.Marca.Find(produto.marcaId);
            }
            produto.marcaNome = produto.marca.id + " - " + produto.marca.nome;
        }

        private UnidadeMedida RecuperaUnidadeMedida(int? id)
        {
            if (id != null)
            {
                UnidadeMedida model = db.UnidadeMedida.Find(id);
                return model;
            }
            return new UnidadeMedida();
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
                Produto model = (Produto)db.Item.Find(id);
                if (model == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    if (model.unidadeMedida == null)
                    {
                        model.unidadeMedida = db.UnidadeMedida.Find(model.unidadeMedidaId);
                    }
                    if (model.marca == null)
                    {
                        model.marca = db.Marca.Find(model.marcaId);
                    }
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

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.unidadeMedidaId = new SelectList(db.UnidadeMedida, "id", "unidade");
            return View();
        }

        // POST: Idiomas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto model)
        {
            if (ModelState.IsValid)
            {
                model.nome = model.nome.ToUpper();
                model.ConfigurarParaSalvar();
                db.Item.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.paisId = new SelectList(db.Pais, "id", "unidade", model.unidadeMedidaId);
            return View(model);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = (Produto)db.Item.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.unidadeMedidaId = new SelectList(db.UnidadeMedida, "id", "unidade", produto.unidadeMedidaId);
            this.ConfiguraNomeMarca(produto);
            produto.unidadeMedida = this.RecuperaUnidadeMedida(produto.unidadeMedidaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.nome = produto.nome.ToUpper();
                produto.ConfigurarParaSalvar();
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.unidadeMedidaId = new SelectList(db.UnidadeMedida, "id", "unidade", produto.unidadeMedidaId);
            ViewBag.marcaId = new SelectList(db.Marca, "id", "nome", produto.marcaId);
            return View(produto);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Produto produto = (Produto)db.Item.Find(id);
                db.Item.Remove(produto);
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

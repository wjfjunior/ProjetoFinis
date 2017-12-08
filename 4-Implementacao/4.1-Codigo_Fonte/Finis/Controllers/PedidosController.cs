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
    public class PedidosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Pedidos
        public ActionResult Index()
        {
            return View(db.Pedido.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        public ActionResult ListaExemplares()
        {
            return PartialView("ListaExemplares");
        }

        //public ActionResult ListaExemplares2()
        //{
        //    var exemplares = db.Exemplar.Include(e => e.editora).Include(e => e.idioma).Include(e => e.sessao).OrderBy(e => e.titulo);

        //    return PartialView("ListaExemplares2", exemplares.ToList());
        //}

        public ActionResult Exportar()
        {
            List<Pedido> pedido = new List<Pedido>();
            pedido = db.Pedido.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Pedidos.rpt"));
            rd.SetDataSource(pedido);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Pedidos.pdf");
        }

        [HttpPost]
        public ActionResult Atualiza(Exemplar exemplar)
        {
                

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult DropboxClientes()
        {
            var listaClientes = db.Cliente.Select(e => new { e.id, e.nome, e.rg }).OrderBy(e => e.nome).ToArray();

            var obj = new
            {
                lista = listaClientes
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraNomeCliente(Avaliacao avaliacao)
        {
            if (avaliacao.cliente == null)
            {
                avaliacao.cliente = db.Cliente.Find(avaliacao.clienteId);
            }
            avaliacao.clienteNome = avaliacao.cliente.id + " - " + avaliacao.cliente.nome + " - " + avaliacao.cliente.rg;
        }


        [HttpGet]
        public JsonResult DropboxExemplares()
        {
            var listaExemplares = db.Item.OfType<Exemplar>().Select(e => new { e.id, e.nome }).OrderBy(e => e.nome).ToArray();

            var obj = new
            {
                lista = listaExemplares
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View(new Pedido());
        }

        // POST: Pedidos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.ConfigurarParaSalvar();
                db.Pedido.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.ConfigurarParaSalvar();
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Pedido pedido = db.Pedido.Find(id);
                db.Pedido.Remove(pedido);
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

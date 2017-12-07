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
    public class VendasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Vendas
        public ActionResult Index()
        {
            return View(db.Venda.Include(a => a.cliente).ToList().OrderByDescending(a => a.dataCompra));
        }

        [HttpPost]
        public ActionResult Index(string pesquisar, DateTime? dataInicio, DateTime? dataFim)
        {
            if (dataInicio != null)
            {
                if (dataFim != null)
                {
                    if (pesquisar != null && pesquisar != "")
                    {
                        if (pesquisar.Contains("-"))
                        {
                            string[] tokens = pesquisar.Split('-');

                            if (tokens.Length == 3)
                            {
                                string nome = tokens[1].Replace(" ", "");
                                string rg = tokens[2].Replace(" ", "");

                                return View(db.Venda
                                .Include(t => t.cliente)
                                .Where(t => t.cliente.nome.Contains(nome) || t.cliente.rg.Contains(rg) && t.dataCompra >= dataInicio && t.dataCompra <= dataFim)
                                .OrderBy(t => t.cliente.nome)
                                .ToList());
                            }
                        }

                        return View(db.Venda
                            .Include(t => t.cliente)
                            .Where(t => t.cliente.nome.Contains(pesquisar) || t.cliente.rg.Contains(pesquisar) && t.dataCompra >= dataInicio && t.dataCompra <= dataFim)
                            .OrderBy(t => t.cliente.nome)
                            .ToList());
                    }
                    else
                    {
                        return View(db.Venda
                            .Include(t => t.cliente)
                            .Where(t => t.dataCompra >= dataInicio && t.dataCompra <= dataFim)
                            .OrderByDescending(t => t.dataCompra)
                            .ToList());
                    }
                }
                else
                {
                    return View(db.Venda
                        .Include(t => t.cliente)
                        .Where(t => t.dataCompra == dataInicio)
                        .OrderByDescending(t => t.dataCompra)
                        .ToList());
                }
            }
            else if (pesquisar != null && pesquisar != "")
            {
                if (pesquisar.Contains("-"))
                {
                    string[] tokens = pesquisar.Split('-');

                    if (tokens.Length == 3)
                    {
                        string nome = tokens[1].Replace(" ", "");
                        string rg = tokens[2].Replace(" ", "");

                        return View(db.Venda
                        .Include(t => t.cliente)
                        .Where(t => t.cliente.nome.Contains(nome) || t.cliente.rg.Contains(rg))
                        .OrderBy(t => t.cliente.nome)
                        .ToList());
                    }
                }

                return View(db.Venda
                    .Include(t => t.cliente)
                    .Where(t => t.cliente.nome.Contains(pesquisar) || t.cliente.rg.Contains(pesquisar))
                    .OrderBy(t => t.cliente.nome)
                    .ToList());
            }
            else
            {
                return View(db.Venda
                    .Include(t => t.cliente)
                    .OrderByDescending(t => t.dataCompra)
                    .ToList());
            }
        }

        // GET: Vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        [HttpGet]
        public JsonResult DropboxItens()
        {
            var listaItens = db.Item.Where((e => e.quantidadeEstoque > 0))
                .Select(e => new { e.id, e.nome})
                .OrderBy(e => e.nome)
                .ToArray();

            var obj = new
            {
                lista = listaItens
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RecuperaCreditosCliente(int? id)
        {
            string creditoEspecial = "";
            string creditoParcial = "";

            if (id != null)
            {
                var cliente = db.Cliente.Find(id);
                if(cliente != null)
                {
                    creditoEspecial = cliente.saldoCreditoEspecialString;
                    creditoParcial = cliente.saldoCreditoParcialString;
                }
            }
            
            var obj = new
            {
                creditoEspecial = creditoEspecial,
                creditoParcial = creditoParcial
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraNomeCliente(Venda venda)
        {
            if (venda.cliente == null)
            {
                venda.cliente = db.Cliente.Find(venda.clienteId);
            }
            venda.clienteNome = venda.cliente.id + " - " + venda.cliente.nome + " - " + venda.cliente.rg;
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            Session["ListaItens"] = new List<ItemVenda>();
            ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome");
            return View(new Venda());
        }

        // POST: Vendas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venda venda)
        {
            if (ModelState.IsValid)
            {
                venda.ConfigurarParaSalvar();
                db.Venda.Add(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", venda.clienteId);
            this.ConfiguraNomeCliente(venda);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", venda.clienteId);
            this.ConfiguraNomeCliente(venda);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Venda venda)
        {
            if (ModelState.IsValid)
            {
                venda.ConfigurarParaSalvar();
                db.Entry(venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", venda.clienteId);
            this.ConfiguraNomeCliente(venda);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda venda = db.Venda.Find(id);
            db.Venda.Remove(venda);
            db.SaveChanges();
            return RedirectToAction("Index");
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

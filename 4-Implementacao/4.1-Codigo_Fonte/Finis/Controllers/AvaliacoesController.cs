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
    public class AvaliacoesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Avaliacoes
        public ActionResult Index()
        {
            var avaliacao = db.Avaliacao.Include(a => a.cliente).OrderByDescending(a => a.dataEntrada);
            return View(avaliacao.ToList());
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

                                return View(db.Avaliacao
                                .Include(t => t.cliente)
                                .Where(t => t.cliente.nome.Contains(nome) || t.cliente.rg.Contains(rg) && t.dataEntrada >= dataInicio && t.dataEntrada <= dataFim)
                                .OrderBy(t => t.cliente.nome)
                                .ToList());
                            }
                        }

                        return View(db.Avaliacao
                            .Include(t => t.cliente)
                            .Where(t => t.cliente.nome.Contains(pesquisar) || t.cliente.rg.Contains(pesquisar) && t.dataEntrada >= dataInicio && t.dataEntrada <= dataFim)
                            .OrderBy(t => t.cliente.nome)
                            .ToList());
                    }
                    else
                    {
                        return View(db.Avaliacao
                            .Include(t => t.cliente)
                            .Where(t => t.dataEntrada >= dataInicio && t.dataEntrada <= dataFim)
                            .OrderByDescending(t => t.dataEntrada)
                            .ToList());
                    }
                }
                else
                {
                    return View(db.Avaliacao
                        .Include(t => t.cliente)
                        .Where(t => t.dataEntrada == dataInicio)
                        .OrderByDescending(t => t.dataEntrada)
                        .ToList());
                }
            }
            else if (pesquisar != null && pesquisar != "")
            {
                if(pesquisar.Contains("-"))
                {
                    string[] tokens = pesquisar.Split('-');

                    if(tokens.Length == 3)
                    {
                        string nome = tokens[1].Replace(" ", "");
                        string rg = tokens[2].Replace(" ", "");

                        return View(db.Avaliacao
                        .Include(t => t.cliente)
                        .Where(t => t.cliente.nome.Contains(nome) || t.cliente.rg.Contains(rg))
                        .OrderBy(t => t.cliente.nome)
                        .ToList());
                    }
                }
                
                return View(db.Avaliacao
                    .Include(t => t.cliente)
                    .Where(t => t.cliente.nome.Contains(pesquisar) || t.cliente.rg.Contains(pesquisar))
                    .OrderBy(t => t.cliente.nome)
                    .ToList());
            }
            else
            {
                return View(db.Avaliacao
                    .Include(t => t.cliente)
                    .OrderByDescending(t => t.dataEntrada)
                    .ToList());
            }
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
                Avaliacao avaliacao = db.Avaliacao.Find(id);
                if (avaliacao == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    if(avaliacao.cliente == null)
                    {
                        avaliacao.cliente = db.Cliente.Find(avaliacao.clienteId);
                    }
                    sucesso = true;
                    resultado = avaliacao.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Avaliacoes/Create
        public ActionResult Create()
        {
            ViewBag.clienteId = new SelectList(db.Cliente, "id", "nome");
            return View();
        }

        public void ConcluiAvaliacao(Avaliacao avaliacao)
        {
            avaliacao.ConcluirAvaliacao();
        }

        // POST: Avaliacoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                db.Avaliacao.Add(avaliacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", avaliacao.clienteId);
            return View(avaliacao);
        }

        // GET: Avaliacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacao.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", avaliacao.clienteId);
            this.ConfiguraNomeCliente(avaliacao);
            return View(avaliacao);
        }

        // POST: Avaliacoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avaliacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clienteId = new SelectList(db.Cliente, "id", "nome", avaliacao.clienteId);
            return View(avaliacao);
        }

        // GET: Clientes/Concluir/5
        public JsonResult ConcluirAvaliacao(int? id)
        {
            bool sucesso = false;
            string resultado = "Não foi possível concluir";

            if (id != null)
            {
                Avaliacao avaliacao = db.Avaliacao.Find(id);
                this.ConfiguraNomeCliente(avaliacao);
                if (avaliacao.status == statusAvaliacao.CANCELADA)
                {
                    sucesso = false;
                    resultado = "Não é possível concluir uma avaliação já cancelada!";
                }
                else if (avaliacao.status == statusAvaliacao.CONCLUIDA)
                {
                    sucesso = false;
                    resultado = "Não é possível concluir uma avaliação já concluída!";
                }
                else
                {
                    avaliacao.CancelarAvaliacao();
                    db.Entry(avaliacao).State = EntityState.Modified;
                    db.SaveChanges();

                    sucesso = true;
                    resultado = "Concluída com sucesso!";
                }
            }

            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Clientes/Cancelar/5
        public JsonResult CancelarAvaliacao(int? id)
        {
            bool sucesso = false;
            string resultado = "Não foi possível cancelar";

            if (id != null)
            {
                Avaliacao avaliacao = db.Avaliacao.Find(id);
                this.ConfiguraNomeCliente(avaliacao);
                if (avaliacao.status == statusAvaliacao.CANCELADA)
                {
                    sucesso = false;
                    resultado = "Não é possível cancelar uma avaliação já cancelada!";
                }
                else if (avaliacao.status == statusAvaliacao.CONCLUIDA)
                {
                    sucesso = false;
                    resultado = "Não é possível cancelar uma avaliação já concluída!";
                }
                else
                {
                    avaliacao.CancelarAvaliacao();
                    db.Entry(avaliacao).State = EntityState.Modified;
                    db.SaveChanges();

                    sucesso = true;
                    resultado = "Cancelado com sucesso!";
                }
            }

            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Avaliacao avaliacao = db.Avaliacao.Find(id);
                this.ConfiguraNomeCliente(avaliacao);
                db.Avaliacao.Remove(avaliacao);
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

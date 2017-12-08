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
    public class TransacoesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Transacoes
        public ActionResult Index()
        {
            var transacoes = db.Transacao.Include(t => t.cliente).OrderByDescending(t => t.data);
            return View(transacoes.ToList());
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

                                return View(db.Transacao
                                .Include(t => t.cliente)
                                .Where(t => t.cliente.nome.Contains(nome) || t.cliente.rg.Contains(rg))
                                .OrderBy(t => t.cliente.nome)
                                .ToList());
                            }
                        }

                        return View(db.Transacao
                            .Include(t => t.cliente)
                            .Where(t => t.cliente.nome.Contains(pesquisar) || t.cliente.rg.Contains(pesquisar) && t.data >= dataInicio && t.data <= dataFim)
                            .OrderBy(t => t.cliente.nome)
                            .ToList());
                    }
                    else
                    {
                        return View(db.Transacao
                            .Include(t => t.cliente)
                            .Where(t => t.data >= dataInicio && t.data <= dataFim)
                            .OrderByDescending(t => t.data)
                            .ToList());
                    }
                }
                else
                {
                    return View(db.Transacao
                        .Include(t => t.cliente)
                        .Where(t => t.data == dataInicio)
                        .OrderByDescending(t => t.data)
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

                        return View(db.Transacao
                        .Include(t => t.cliente)
                        .Where(t => t.cliente.nome.Contains(nome) || t.cliente.rg.Contains(rg))
                        .OrderBy(t => t.cliente.nome)
                        .ToList());
                    }
                }

                return View(db.Transacao
                    .Include(t => t.cliente)
                    .Where(t => t.cliente.nome.Contains(pesquisar) || t.cliente.rg.Contains(pesquisar))
                    .OrderBy(t => t.cliente.nome)
                    .ToList());
            }
            else
            {
                return View(db.Transacao
                    .Include(t => t.cliente)
                    .OrderByDescending(t => t.data)
                    .ToList());
            }
        }

        public ActionResult Exportar()
        {
            List<Transacao> transacao = new List<Transacao>();
            transacao = db.Transacao.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Transacoes.rpt"));
            rd.SetDataSource(transacao);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Transacoes.pdf");
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
                Transacao transacao = db.Transacao.Find(id);
                if (transacao == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    if(transacao.cliente == null)
                    {
                        transacao.cliente = db.Cliente.Find(transacao.clienteId);
                    }
                    sucesso = true;
                    resultado = transacao.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        
        public void GeraTransacaoEntrada(decimal? valor, TipoCredito credito, int? clienteId)
        {
            Transacao transacao = new Transacao();
            transacao.NovaTransacaoEntrada(valor, credito, clienteId);
            transacao.ConfigurarParaSalvar();
            db.Transacao.Add(transacao);
            db.SaveChanges();
        }

        public void GeraTransacaoSaida(decimal? valor, TipoCredito credito, int? clienteId)
        {
            Transacao transacao = new Transacao();
            transacao.NovaTransacaoEntrada(valor, credito, clienteId);
            transacao.ConfigurarParaSalvar();
            db.Transacao.Add(transacao);
            db.SaveChanges();
        }
    }
}
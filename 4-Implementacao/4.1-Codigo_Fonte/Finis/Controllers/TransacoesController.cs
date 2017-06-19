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
    public class TransacoesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Transacoes
        public ActionResult Index()
        {
            var transacoes = db.Transacao.Include(t => t.cliente);
            return View(transacoes.ToList());
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
        
        public void GeraTransacaoEntrada(decimal valor, TipoCredito credito, int? clienteId)
        {
            Transacao transacao = new Transacao();
            transacao.NovaTransacaoEntrada(valor, credito, clienteId);
            db.Transacao.Add(transacao);
            db.SaveChanges();
        }

        public void GeraTransacaoSaida(decimal valor, TipoCredito credito, int? clienteId)
        {
            Transacao transacao = new Transacao();
            transacao.NovaTransacaoEntrada(valor, credito, clienteId);
            db.Transacao.Add(transacao);
            db.SaveChanges();
        }
    }
}
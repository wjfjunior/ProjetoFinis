using Finis.DAL;
using Finis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finis.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário, Cliente")]
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Totais()
        {
            int totClientes = db.Cliente.Count();
            int totExemplares = db.Item.OfType<Exemplar>().Count();
            int totProdutos = db.Item.OfType<Produto>().Count();
            int totServicos = db.Item.OfType<Produto>().Count();

            var obj = new
            {
                TotalClientes = totClientes.ToString(),
                TotalExemplares = totExemplares.ToString(),
                TotalProdutos = totProdutos.ToString(),
                TotalServicos = totServicos.ToString()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalPedidos()
        {
            int pedidosPendentes = db.Pedido.Where(p => p.situacao == Models.situacaoPedido.PENDENTE).Count();
            int pedidosRealizados = db.Pedido.Where(p => p.situacao == Models.situacaoPedido.REALIZADO).Count();
            int pedidosAguardando = db.Pedido.Where(p => p.situacao == Models.situacaoPedido.AGUARDANDO_CLIENTE).Count();
            int pedidosConcluidos = db.Pedido.Where(p => p.situacao == Models.situacaoPedido.CONCLUIDO).Count();

            var obj = new
            {
                PedidosPendentes = pedidosPendentes.ToString(),
                PedidosRealizados = pedidosRealizados.ToString(),
                PedidosAguardando = pedidosAguardando.ToString(),
                PedidosConcluidos = pedidosConcluidos.ToString()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalAvaliacoes()
        {
            int avaliacoesAguardando = db.Avaliacao.Where(p => p.situacao == Models.situacaoAvaliacao.AGUARDANDO_AVALIACAO).Count();
            int avaliacoesAvaliado = db.Avaliacao.Where(p => p.situacao == Models.situacaoAvaliacao.AVALIADO).Count();
            int avaliacoesAguardCliente = db.Avaliacao.Where(p => p.situacao == Models.situacaoAvaliacao.AGUARDANDO_CLIENTE).Count();
            int avaliacoesConcluido = db.Avaliacao.Where(p => p.situacao == Models.situacaoAvaliacao.CONCLUIDO).Count();
            int avaliacoesCancelada = db.Avaliacao.Where(p => p.situacao == Models.situacaoAvaliacao.CANCELADA).Count();

            var obj = new
            {
                AvaliacoesAguardando = avaliacoesAguardando.ToString(),
                AvaliacoesAvaliado = avaliacoesAvaliado.ToString(),
                AvaliacoesAguardCliente = avaliacoesAguardCliente.ToString(),
                AvaliacoesConcluido = avaliacoesConcluido.ToString(),
                AvaliacoesCancelada = avaliacoesCancelada.ToString()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraInicioFimMes(DateTime primeiroDia, DateTime ultimoDia, int mes)
        {
            primeiroDia = new DateTime(DateTime.Now.Year, mes, 1);
            ultimoDia = new DateTime(DateTime.Now.Year, mes, DateTime.DaysInMonth(DateTime.Now.Year, mes));
        }

        public JsonResult TotalTransacoes()
        {
            DateTime dataInicio = DateTime.Today;
            DateTime dataFim = dataInicio;
            
            IList<Transacao> listaTransacoes = new List<Transacao>();
            IList<TotalTransacao> listaTotais = new List<TotalTransacao>();

            int mes = DateTime.Now.Month;

            for (int i = 0; i < 12; i++)
            {
                TotalTransacao totalTransacao = new TotalTransacao();
                this.ConfiguraInicioFimMes(dataInicio, dataFim, mes);

                totalTransacao.totalEntrada = db.Transacao.Where(t => t.tipoTransacao == Models.TipoTransacao.ENTRADA 
                                                                && t.data >= dataInicio 
                                                                && t.data <= dataFim).Count();

                totalTransacao.totalSaida = db.Transacao.Where(t => t.tipoTransacao == Models.TipoTransacao.SAIDA
                                                                && t.data >= dataInicio
                                                                && t.data <= dataFim).Count();

                totalTransacao.mesReferencia = dataInicio;
                listaTotais.Add(totalTransacao);
                DateTime DtaAux = new DateTime(DateTime.Now.Year, mes, DateTime.Now.Day);
                mes = DtaAux.AddMonths(-1).Month;
            }

            var obj = new
            {
                lista = listaTotais
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
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
        
        [HttpGet]
        public JsonResult CalculaValores(string desconto, string descontoPorcentagem, string subtotal, string creditoParcial,
            string creditoEspecial, string total, string recebido, string troco)
        {
            IList<ItemVenda> listaItens = (List<ItemVenda>)Session["ListaItens"];

            bool resultado;
            Decimal Desconto = Decimal.Parse(desconto.Replace(".", ","));
            Decimal DescontoPorcentagem = Decimal.Parse(descontoPorcentagem);
            Decimal Subtotal = 0;
            Decimal CreditoParcial = Decimal.Parse(creditoParcial.Replace(".", ","));
            Decimal CreditoEspecial = Decimal.Parse(creditoEspecial.Replace(".", ","));
            Decimal Total = 0;
            Decimal Recebido = Decimal.Parse(recebido);
            Decimal Troco = 0;

            if (listaItens != null)
            {
                foreach(ItemVenda item in listaItens)
                {
                    Subtotal += item.precoTotal;
                }
            }

            Total = Subtotal;
            Total -= Convert.ToDecimal(((double)DescontoPorcentagem / 100) * Convert.ToDouble(Total));
            Total -= Desconto;
            Total -= CreditoParcial;
            Total -= CreditoEspecial;

            Troco = Recebido - Total;

            if(Troco < 0)
            {
                Troco = 0;
            }

            if(Total > 0)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            var obj = new
            {
                Resultado = resultado,
                Desconto = Desconto.ToString(),
                DescontoPorcentagem = DescontoPorcentagem.ToString(),
                Subtotal = Subtotal.ToString(),
                CreditoParcial = CreditoParcial.ToString(),
                CreditoEspecial = CreditoEspecial.ToString(),
                Total = Total.ToString(),
                Recebido = Recebido.ToString(),
                Troco = Troco.ToString()
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
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

        //private void ConfiguraNomeCliente(Venda venda)
        //{
        //    if (venda.cliente == null)
        //    {
        //        venda.cliente = db.Cliente.Find(venda.clienteId);
        //    }
        //    venda.clienteNome = venda.cliente.id + " - " + venda.cliente.nome + " - " + venda.cliente.rg;
        //}

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
            var itensVenda = (List<ItemVenda>)Session["ListaItens"];

            if(!this.ValidaCreditosCliente(venda))
            {
                ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", venda.clienteId);
                @ViewBag.Erro = "O valor de lacamento de creditos de desconto do cliente e maior que o saldo disponivel para o mesmo!";
                return View(venda);
            }

            if (itensVenda != null && itensVenda.Count() != 0)
            {
                if (ModelState.IsValid)
                {
                    venda.ConfigurarParaSalvar();
                    db.Venda.Add(venda);
                    db.SaveChanges();
                    this.SalvarItens(venda, itensVenda);
                    this.GeraTransacoesSaida(venda);
                    this.DescontaQuantidade(itensVenda);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", venda.clienteId);
                @ViewBag.Erro = "Nao e possivel encerrar o registro de venda sem o lancamento de pelo menos um item!";
            }
            return View(venda);
        }

        public void DescontaQuantidade(List<ItemVenda> lista)
        {
            ExemplaresController exemplares = new ExemplaresController();

            exemplares.DescontaQuantidade(lista);
        }

        public void GeraTransacoesSaida(Venda venda)
        {
            var cliente = db.Cliente.Find(venda.clienteId);

            if(cliente != null)
            {
                cliente.AtualizaSaidaSaldoEspecial(venda.creditoEspecial);
                cliente.AtualizaSaidaSaldoParcial(venda.creditoParcial);
            }
        }

        public bool ValidaCreditosCliente(Venda venda)
        {
            var cliente = db.Cliente.Find(venda.clienteId);

            if ((venda.creditoEspecial > cliente.saldoCreditoEspecial) || venda.creditoParcial > cliente.saldoCreditoParcial)
                return false;
            else
                return true;
        }

        public void SalvarItens(Venda venda, List<ItemVenda> lista)
        {
            foreach(ItemVenda item in lista)
            {
                item.vendaId = venda.id;
                item.venda = venda;

                db.ItemVenda.Add(item);
                db.SaveChanges();
            }
        }

        public ActionResult Exportar()
        {
            List<Venda> venda = new List<Venda>();
            venda = db.Venda.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Vendas.rpt"));
            rd.SetDataSource(venda);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Vendas.pdf");
        }

        // GET: Vendas/Edit/5
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
            ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", venda.clienteId);
            this.CarregaLista(venda);
            return View(venda);
        }

        public void CarregaLista(Venda venda)
        {
            listaItens = db.ItemVenda.Where(e => e.vendaId == venda.id).OrderBy(e => e.indice).ToList();

            foreach(ItemVenda itemVenda in listaItens)
            {
                var item = db.Item.Find(itemVenda.itemId);

                itemVenda.item.nome = item.nome;
                itemVenda.item.precoVenda = item.precoVenda;
            }

            Session["ListaItens"] = listaItens;
            venda.itensVenda = listaItens;
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
                var itensVenda = (List<ItemVenda>)Session["ListaItens"];
                venda.ConfigurarParaSalvar();
                db.Entry(venda).State = EntityState.Modified;
                this.SalvarEditarItens(venda, itensVenda);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Clientes = new SelectList(db.Cliente, "id", "nome", venda.clienteId);
            //this.ConfiguraNomeCliente(venda);
            return View(venda);
        }

        public void SalvarEditarItens(Venda venda, List<ItemVenda> lista)
        {
            foreach (ItemVenda item in lista)
            {
                item.vendaId = venda.id;
                item.venda = venda;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
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

        //------------------------------------------------------
        private List<ItemVenda> listaItens = new List<ItemVenda>();

        public ActionResult ListarItens()
        {
            return PartialView("ListarItens", listaItens);
        }

        public void IncializarLista()
        {
            List<ItemVenda> listaItens = new List<ItemVenda>();
            Session["ListaItens"] = listaItens;
        }

        public List<ItemVenda> RecuperarLista()
        {
            listaItens = (List<ItemVenda>)Session["ListaItens"];

            if (listaItens == null)
            {
                listaItens = new List<ItemVenda>();
            }

            return listaItens;
        }

        public List<ItemVenda> InsereLista(ItemVenda item)
        {
            listaItens = this.RecuperarLista();
            listaItens.Add(item);
            this.AtualizaIndiceItem(listaItens);
            Session["ListaItens"] = listaItens;

            return listaItens;
        }

        public void AtualizaIndiceItem(List<ItemVenda> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].indice = i + 1;
            }
        }

        [HttpGet]
        public ActionResult AdicionarItem(int id)
        {
            var item = db.Item.Find(id);
            ItemVenda itemVenda = new ItemVenda();
            itemVenda.item = item;
            itemVenda.precoTotal = itemVenda.item.precoVenda * itemVenda.quantidade;
            this.InsereLista(itemVenda);

            return PartialView("ListarItens", listaItens);
        }

        [HttpGet]
        public ActionResult EditarItem(int indice, int quantidade)
        {
            listaItens = this.RecuperarLista();
            listaItens[indice - 1].quantidade = quantidade;
            listaItens[indice - 1].precoTotal = listaItens[indice - 1].item.precoVenda * listaItens[indice - 1].quantidade;
            Session["ListaItens"] = listaItens;

            return PartialView("ListarItens", listaItens);
        }

        [HttpGet]
        public ActionResult RemoverItem(int indice)
        {
            listaItens = this.RecuperarLista();
            listaItens.RemoveAt(indice - 1);
            this.AtualizaIndiceItem(listaItens);
            Session["ListaItens"] = listaItens;

            return PartialView("ListarItens", listaItens);
        }

        [HttpGet]
        public JsonResult QuantidadeMaiorQueEstoque(int id)
        {
            int quantidadeItem = 0;
            bool resultado;

            var item = db.Item.Find(id);
            listaItens = this.RecuperarLista();

            foreach (ItemVenda itemVenda in listaItens)
            {
                if (item.id == itemVenda.item.id)
                {
                    quantidadeItem += itemVenda.quantidade;
                }
            }

            if (quantidadeItem >= item.quantidadeEstoque)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            var obj = new
            {
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VerificaQuantidadeItem(int indice, int quantidade)
        {
            int quantidadeItem = 0;
            bool resultado;

            listaItens = this.RecuperarLista();
            var item = listaItens[indice - 1].item;
            listaItens[indice - 1].quantidade = quantidade;

            foreach (ItemVenda itemVenda in listaItens)
            {
                if (item.id == itemVenda.item.id)
                {
                    quantidadeItem += itemVenda.quantidade;
                }
            }

            if (quantidadeItem > item.quantidadeEstoque)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            var obj = new
            {
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
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
                Item item = db.Item.Find(id);
                if (item == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = item.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DropboxItens(string item)
        {
            var listaItens = db.Item.Where(i => i.nome.Contains(item) || i.id.Equals(item)).ToList();

            return Json(listaItens, "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}

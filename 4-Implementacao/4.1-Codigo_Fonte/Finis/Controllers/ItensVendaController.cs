using Finis.DAL;
using Finis.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finis.Controllers
{
    public class ItensVendaController : Controller
    {
        private Contexto db = new Contexto();
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

            if(listaItens == null)
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
            for(int i = 0; i < lista.Count; i++)
            {
                lista[i].indice = i+1;
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

            if(quantidadeItem >= item.quantidadeEstoque)
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
        public JsonResult DropboxItens (string item)
        {
            var listaItens = db.Item.Where(i => i.nome.Contains(item) || i.id.Equals(item)).ToList();
            
            return Json(listaItens, "text/html", JsonRequestBehavior.AllowGet);
        }

    }
}
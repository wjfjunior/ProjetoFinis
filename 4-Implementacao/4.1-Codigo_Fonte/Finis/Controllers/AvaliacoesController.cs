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
            var avaliacao = db.Avaliacao.Include(a => a.cliente);
            return View(avaliacao.ToList());
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
        public ActionResult Create([Bind(Include = "id,dataEntrada,quantidadeExemplares,creditoEspecial,creditoParcial,situacao,clienteId,user_insert,user_update,date_insert,date_update")] Avaliacao avaliacao)
        {
            if(avaliacao.situacao == situacaoAvaliacao.CONCLUIDO)
            {
                ViewBag.Erro = 1;
                return View(avaliacao);
            }

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
            return View(avaliacao);
        }

        // POST: Avaliacoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dataEntrada,quantidadeExemplares,creditoEspecial,creditoParcial,situacao,clienteId,user_insert,user_update,date_insert,date_update")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avaliacao).State = EntityState.Modified;
                db.SaveChanges();
                if (avaliacao.situacao == situacaoAvaliacao.CONCLUIDO)
                {
                    this.ConcluiAvaliacao(avaliacao);
                }
                return RedirectToAction("Index");
            }
            ViewBag.clienteId = new SelectList(db.Cliente, "id", "nome", avaliacao.clienteId);
            return View(avaliacao);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Avaliacao avaliacao = db.Avaliacao.Find(id);
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

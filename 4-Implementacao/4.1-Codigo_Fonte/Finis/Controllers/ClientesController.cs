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
using Newtonsoft.Json;

namespace Finis.Controllers
{
    public class ClientesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes
        public ActionResult Buscar(string nome)
        {
            return View("Index", db.Cliente.Where(c => c.nome.Contains(nome)).ToList());
        }

        private Endereco RecuperaEndereco(int? id)
        {
            if(id != null)
            {
                Endereco endereco = db.Endereco.Find(id);
                return endereco;
            }
            return new Endereco();
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
                Cliente cliente = db.Cliente.Find(id);
                if (cliente == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    cliente.endereco = RecuperaEndereco(cliente.enderecoId);
                    sucesso = true;
                    resultado = cliente.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.Cidades = new SelectList(db.Cidade, "Id", "Nome", "Estado");
            return View();
        }

        // POST: Clientes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente model)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cidades = new SelectList(db.Cidade, "Id", "Nome", "Estado");
            return View(model);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cidades = new SelectList(db.Cidade, "Id", "Nome", "Estado");
            var enderecoID = cliente.enderecoId;
            cliente.endereco = this.RecuperaEndereco(cliente.enderecoId);
            cliente.enderecoId = enderecoID;
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente model)
        {
            if (ModelState.IsValid)
            {
                model.endereco.id = model.enderecoId;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cidades = new SelectList(db.Cidade, "Id", "Nome", "Estado");
            return View(model);
        }

        public void AtualizaSaldoEspecial(int? id, decimal? creditoEspecial)
        {
            if(id != null)
            {
                Cliente cliente = db.Cliente.Find(id);
                if (cliente != null)
                {
                    cliente.AtualizaSaldoEspecial(creditoEspecial.GetValueOrDefault());
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void AtualizaSaldoParcial(int? id, decimal? creditoParcial)
        {
            if (id != null)
            {
                Cliente cliente = db.Cliente.Find(id);
                if (cliente != null)
                {
                    cliente.AtualizaSaldoParcial(creditoParcial.GetValueOrDefault());
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void NovoSaldo(int? id, decimal creditoEspecial, decimal creditoParcial)
        {
            if (id != null)
            {
                Cliente cliente = db.Cliente.Find(id);
                if (cliente != null)
                {
                    cliente.NovoSaldo(creditoEspecial, creditoParcial);
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cidades = new SelectList(db.Cidade, "Id", "Nome", "Estado");
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if(id != null)
            {
                Cliente cliente = db.Cliente.Find(id);
                Endereco endereco = this.RecuperaEndereco(cliente.enderecoId);
                db.Cliente.Remove(cliente);
                db.Endereco.Remove(endereco);
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

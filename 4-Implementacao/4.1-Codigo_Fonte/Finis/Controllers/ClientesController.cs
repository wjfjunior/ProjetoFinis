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
            ViewBag.Title = "Cadastro de Clientes";
            return View(db.Clientes.ToList());
        }

        // GET: Clientes
        public ActionResult Buscar(string nome)
        {
            return View("Index", db.Clientes.Where(c => c.nome.Contains(nome)).ToList());
        }

        private Endereco RecuperaEndereco(int? id)
        {
            if(id != null)
            {
                Endereco endereco = db.Enderecos.Find(id);
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
                Cliente cliente = db.Clientes.Find(id);
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
            ViewBag.Cidades = new SelectList(db.Clientes, "Id", "Nome", "Estado");
            ViewBag.Title = "Inserir Cliente";
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
                db.Clientes.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cidades = new SelectList(db.Clientes, "Id", "Nome", "Estado");
            ViewBag.Title = "Inserir Cliente";
            return View(model);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cidades = new SelectList(db.Clientes, "Id", "Nome", "Estado");
            var enderecoID = cliente.enderecoId;
            cliente.endereco = this.RecuperaEndereco(cliente.enderecoId);
            cliente.enderecoId = enderecoID;
            ViewBag.Title = "Editar Cliente";
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
            ViewBag.Cidades = new SelectList(db.Cidades, "Id", "Nome", "Estado");
            ViewBag.Title = "Editar Cliente";
            return View(model);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cidades = new SelectList(db.Cidades, "Id", "Nome", "Estado");
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if(id != null)
            {
                Cliente cliente = db.Clientes.Find(id);
                Endereco endereco = this.RecuperaEndereco(cliente.enderecoId);
                db.Clientes.Remove(cliente);
                db.Enderecos.Remove(endereco);
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
﻿using System;
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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Finis.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")]
    public class ClientesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Cliente.Where(c => c.ativo == true).ToList().OrderBy(c => c.nome));
        }

        [HttpPost]
        public ActionResult Index(string pesquisar, bool ativo)
        {
            return View("Index", db.Cliente.Where(c => c.ativo.Equals(ativo) && (c.rg.Contains(pesquisar) || c.nome.Contains(pesquisar))).ToList());
        }

        public ActionResult Exportar()
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes = db.Cliente.ToList();
            
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Relatorios"), "Clientes.rpt"));
            rd.SetDataSource(clientes);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Clientes.pdf");
        }

        [HttpGet]
        public JsonResult DropboxCidades()
        {
            var listaCidades = db.Cidade.Select(c => new { c.id, c.nome, c.estado.sigla }).OrderBy(c => c.nome).ToArray();

            var obj = new
            {
                lista = listaCidades
            };

            return Json(obj, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void ConfiguraNomeCidade(Cliente cliente)
        {
            if (cliente.endereco == null || cliente.endereco.id == 0)
            {
                cliente.endereco = db.Endereco.Find(cliente.enderecoId);
            }
            if (cliente.endereco.cidade == null || cliente.endereco.cidadeId == null || cliente.endereco.cidade.id == 0)
            {
                cliente.endereco.cidade = db.Cidade.Find(cliente.endereco.cidadeId);
            }
            if (cliente.endereco.cidade.estado == null || cliente.endereco.cidade.estadoId == 0 || cliente.endereco.cidade.estado.id == 0)
            {
                cliente.endereco.cidade.estado = db.Estado.Find(cliente.endereco.cidade.estadoId);
            }
            cliente.endereco.cidadeNome = cliente.endereco.cidade.id + " - " + cliente.endereco.cidade.nome + "/" + cliente.endereco.cidade.estado.sigla;
        }

        private bool VerificaJaExiste(Cliente cliente)
        {
            List<Cliente> resultado = new List<Cliente>();

            resultado = db.Cliente.Where(c => c.nome == cliente.nome && c.rg == cliente.rg).ToList();
            if (resultado.Count() > 0)
            {
                return true;
            }

            return false;
        }

        private Endereco RecuperaEndereco(int? id)
        {
            if(id != null)
            {
                Endereco endereco = db.Endereco.Find(id);
                if(endereco.cidade == null)
                {
                    endereco.cidade = db.Cidade.Find(endereco.cidadeId);
                }
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
                if (VerificaJaExiste(model))
                {
                    ViewBag.Erro = "Ja existe um registro com os valores informados!";
                    return View(model);
                }
                model.ConfigurarParaSalvar();
                db.Cliente.Add(model);
                db.SaveChanges();
                VerificaSaldo(model);
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
            this.ConfiguraNomeCidade(cliente);
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
                VerificaSaldo(model);
                model.ConfigurarParaSalvar();
                
                var local = db.Set<Cliente>()
                         .Local
                         .FirstOrDefault(f => f.id == model.id);

                db.Entry(local).State = System.Data.Entity.EntityState.Detached;
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.Cidades = new SelectList(db.Cidade, "Id", "Nome", "Estado");
            return View(model);
        }

        public void VerificaSaldo(Cliente model)
        {
            if (model.id != null)
            {
                Cliente cliente = db.Cliente.Find(model.id);
                if (cliente != null)
                {
                    if (cliente.saldoCreditoEspecial != model.saldoCreditoEspecial)
                    {
                        cliente.NovoSaldoEspecial(model.saldoCreditoEspecial);
                        db.Entry(cliente).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    if (cliente.saldoCreditoParcial != model.saldoCreditoParcial)
                    {
                        cliente.NovoSaldoParcial(model.saldoCreditoParcial);
                        db.Entry(cliente).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
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
                Cliente model = db.Cliente.Find(id);
                //Endereco endereco = this.RecuperaEndereco(cliente.enderecoId);
                //db.Cliente.Remove(cliente);
                //db.Endereco.Remove(endereco);
                model.ativo = false;

                var local = db.Set<Cliente>()
                         .Local
                         .FirstOrDefault(f => f.id == model.id);

                db.Entry(local).State = System.Data.Entity.EntityState.Detached;
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException != null)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
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

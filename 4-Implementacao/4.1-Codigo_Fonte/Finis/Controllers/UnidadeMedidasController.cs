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

namespace Finis.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")]
    public class UnidadeMedidasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: UnidadeMedidas
        public ActionResult Index()
        {
            return View(db.UnidadeMedida.OrderBy(u => u.grandeza).ToList());
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.UnidadeMedida.Where(c => c.grandeza.Contains(pesquisar)).ToList());
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
                UnidadeMedida unidadeMedida = db.UnidadeMedida.Find(id);
                if (unidadeMedida == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = unidadeMedida.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: UnidadeMedidas/Create
        public ActionResult Create()
        {
            return View();
        }

        private bool VerificaJaExiste(UnidadeMedida model)
        {
            List<UnidadeMedida> resultado = new List<UnidadeMedida>();

            resultado = db.UnidadeMedida.Where(m => m.grandeza == model.grandeza).ToList();
            if (resultado.Count() > 0)
            {
                return true;
            }

            return false;
        }

        // POST: Paises/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UnidadeMedida model)
        {
            if (ModelState.IsValid)
            {
                model.simbolo = model.simbolo.ToUpper();
                if (VerificaJaExiste(model))
                {
                    ViewBag.Erro = "Já existe um registro com os valores informados!";
                    return View(model);
                }
                db.UnidadeMedida.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: UnidadeMedidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadeMedida unidadeMedida = db.UnidadeMedida.Find(id);
            if (unidadeMedida == null)
            {
                return HttpNotFound();
            }
            return View(unidadeMedida);
        }

        // POST: Paises/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UnidadeMedida model)
        {
            if (ModelState.IsValid)
            {
                model.simbolo = model.simbolo.ToUpper();
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Clientes/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                UnidadeMedida unidadeMedida = db.UnidadeMedida.Find(id);
                db.UnidadeMedida.Remove(unidadeMedida);
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
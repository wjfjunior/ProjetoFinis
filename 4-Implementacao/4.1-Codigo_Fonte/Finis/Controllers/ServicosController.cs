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
    [Authorize(Roles = "Administrador, Funcionário")]
    public class ServicosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Servicos
        public ActionResult Index()
        {
            return View(db.Item.OfType<Servico>().ToList().OrderBy(s => s.nome));
        }

        [HttpPost]
        public ActionResult Index(string pesquisar)
        {
            return View("Index", db.Item.OfType<Servico>().Where(p => p.nome.Contains(pesquisar)).ToList().OrderBy(p => p.nome));
        }

        // GET: Servicos/Details/5
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
                Servico model = (Servico)db.Item.Find(id);
                if (model == null)
                {
                    sucesso = false;
                    resultado = "Não encontrado!";
                }
                else
                {
                    sucesso = true;
                    resultado = model.Serializar();
                }
            }
            var obj = new
            {
                Sucesso = sucesso,
                Resultado = resultado
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        private bool VerificaJaExiste(Servico usuario)
        {
            List<Servico> resultado = new List<Servico>();

            resultado = db.Item.OfType<Servico>().Where(e => e.nome.Equals(usuario.nome)).ToList();
            if (resultado.Count() > 0)
            {
                return true;
            }

            return false;
        }

        // GET: Servicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.nome = servico.nome.ToUpper();
                if (VerificaJaExiste(servico))
                {
                    ViewBag.Erro = "Já existe um registro com o nome informado!";
                    return View(servico);
                }
                servico.ConfigurarParaSalvar();
                db.Item.Add(servico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Servicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = (Servico)db.Item.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servicos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.ConfigurarParaSalvar();
                db.Entry(servico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Usuarios/Delete/5
        public JsonResult DeletarRegistro(int? id)
        {
            if (id != null)
            {
                Servico servico = (Servico)db.Item.Find(id);
                db.Item.Remove(servico);
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

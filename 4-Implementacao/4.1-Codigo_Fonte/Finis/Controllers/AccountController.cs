using Finis.DAL;
using Finis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Finis.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        /// <param name="returnURL"></param>
        /// <returns></returns>
        public ActionResult Login(string returnURL)
        {
            ViewBag.ReturnUrl = returnURL;
            return View(new Usuario());
        }

        /// <param name="login"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(Usuario login, string returnUrl)
         {
            //if (ModelState.IsValid)
            //{
                 using (Contexto db = new Contexto())
                 {
                    var vLogin = db.Usuario.Where(p => p.email.Equals(login.email)).FirstOrDefault();
                     /*Verificar se a variavel vLogin está vazia. Isso pode ocorrer caso o usuário não existe. 
                     Caso não exista ele vai cair na condição else.*/
                     if (vLogin != null)
                          {
                             /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                               ativo. Caso não esteja cai direto no else*/
                              if (vLogin.ativo) //Equals(vLogin.ativo, "S")
                    {
                                  /*Código abaixo verifica se a senha digitada no site é igual a senha que está sendo retornada 
                                   do banco. Caso não cai direto no else*/
                                 if (Equals(vLogin.senha, login.senha))
                                  {
                                    FormsAuthentication.SetAuthCookie(vLogin.email, false);
                                      if (Url.IsLocalUrl(returnUrl)
                                      && returnUrl.Length > 1
                                      && returnUrl.StartsWith("/")
                                      && !returnUrl.StartsWith("//")
                                      && returnUrl.StartsWith("/\\"))
                                      {
                                       return Redirect(returnUrl);
                                      }
                                 /*código abaixo cria uma session para armazenar o nome do usuário*/
                                 Session["Nome"] = vLogin.nome;
                                /*código abaixo cria uma session para armazenar o sobrenome do usuário*/
                                 Session["Sobrenome"] = vLogin.sobrenome;
                                 /*retorna para a tela inicial do Home*/
                                 return RedirectToAction("Index", "Home");
                             }
                             /*Else responsável da validação da senha*/
                             else
                             {
                                 /*Escreve na tela a mensagem de erro informada*/
                                 ModelState.AddModelError("", "Senha inválida!");
                                 /*Retorna a tela de login*/
                                 return View(new Usuario());
                             }
                         }
                        /*Else responsável por verificar se o usuário está ativo*/
                         else
                          {
                            /*Escreve na tela a mensagem de erro informada*/
                             ModelState.AddModelError("", "Usuário sem acesso ao sistema!");
                             /*Retorna a tela de login*/
                             return View(new Usuario());
                         }
                    }
                    /*Else responsável por verificar se o usuário existe*/
                     else
                     {
                         /*Escreve na tela a mensagem de erro informada*/
                         ModelState.AddModelError("", "E-mail não cadastrado!");
                              /*Retorna a tela de login*/
                         return View(new Usuario());
                     }
                 }
             //}
             /*Caso os campos não esteja de acordo com a solicitação retorna a tela de login com as mensagem dos campos*/
             //return View(login);
         }

        public ActionResult LogOff()
        {
            Request.Cookies.Remove(User.Identity.Name);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
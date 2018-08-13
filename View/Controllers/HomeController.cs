using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json.Linq;
using View.Util;

namespace View.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login) {
            var webClient = new WebClient();

            Stream stream = null;

            try {
                stream = webClient.OpenRead(APIUrl.Login(login));
            } catch (WebException ex) {
                if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.NotFound) {
                    ModelState.AddModelError(string.Empty, "Usário não encontrado.");
                    return View();
                }

                if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.BadRequest) {
                    ModelState.AddModelError(string.Empty, "Informe um login.");
                    return View();
                }
            }

            var retorno = new StreamReader(stream).ReadToEnd();

            var obj = JObject.Parse(retorno);

            var usuario = Helpers.ToUsuario(obj);

            FormsAuthentication.SetAuthCookie(usuario.Id, false);

            var authTicket = new FormsAuthenticationTicket(1, usuario.Id, DateTime.Now, DateTime.Now.AddMinutes(20), false, usuario.Tipo);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(authCookie);

            var infoCookie = new HttpCookie("infoCookie", usuario.Nome);
            Response.Cookies.Add(infoCookie);

            if (usuario.Tipo == "Professor")
                return RedirectToAction("Index", "Professor");

            return RedirectToAction("Index", "Aluno");
        }

        public ActionResult Sair() {
            FormsAuthentication.SignOut();

            var infoCookie = new HttpCookie("infoCookie") {Expires = DateTime.Now.AddMinutes(-1)};
            Response.Cookies.Add(infoCookie);

            return RedirectToAction("Index");
        }
    }
}
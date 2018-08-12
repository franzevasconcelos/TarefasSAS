using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using View.Models;

namespace View.Util
{
    public static class Helpers
    {
        public static Usuario ToUsuario(JObject obj) {
            var objeto = obj as dynamic;

            return new Usuario {
                                   Id = objeto.Id,
                                   Nome = objeto.Nome,
                                   Tipo = objeto.TipoLogin.ToString() == "0" ? "Professor" : "Aluno"
                               };
        }

        public static MvcHtmlString ObterNome() {
            var cookie = HttpContext.Current.Request.Cookies.Get("infoCookie");

            if (cookie == null) return null;

            return new MvcHtmlString(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cookie.Value));
        }
    }
}
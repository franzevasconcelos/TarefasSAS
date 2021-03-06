﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using View.Mappers;

namespace View {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(QuestaoMapper).Assembly));
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e) {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            if (authTicket == null || authTicket.Expired) return;

            var roles = authTicket.UserData.Split(',');
            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
        }
    }
}
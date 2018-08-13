using System;
using System.Web.Http;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Controllers;
using TarefasSAS.API.Mappers;

namespace TarefasSAS.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e) {
            var log = log4net.LogManager.GetLogger(typeof(TarefasController));

            var ex = Server.GetLastError();
            log.Debug(ex.Message, ex);

        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            NhibernateSetup.Init();
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(QuestaoMapper).Assembly));
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}

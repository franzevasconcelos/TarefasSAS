using System.Web.Http;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Mappers;

namespace TarefasSAS.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            NhibernateSetup.Init();
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(QuestaoMapper).Assembly));
        }
    }
}

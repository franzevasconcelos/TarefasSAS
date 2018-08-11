using System.Web.Http;
using TarefasSAS.API.Configuracoes;

namespace TarefasSAS.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            NhibernateSetup.Init();
        }
    }
}

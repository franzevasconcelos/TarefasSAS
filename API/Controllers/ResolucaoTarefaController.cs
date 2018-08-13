using System.Web.Http;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Controllers {
    public class ResolucaoTarefaController : ApiController {
        private readonly Resolucoes _resolucoes;

        public ResolucaoTarefaController(Resolucoes resolucoes) {
            _resolucoes = resolucoes;
        }

        public ResolucaoTarefaController() : this(new Resolucoes(NhibernateSetup.GetSession())) { }

        [HttpPut]
        public IHttpActionResult LiberarParaCorrecao(Interface.Resolucao resolucao) {
            var resolucaoTarefa = _resolucoes.ResolucaoTarefaPorId(resolucao.Id);

            resolucaoTarefa.Enviada = true;
            _resolucoes.SalvarResolucaoTarefa(resolucaoTarefa);

            return Ok();
        }
    }
}
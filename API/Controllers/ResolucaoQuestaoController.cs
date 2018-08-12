using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Controllers
{
    public class ResolucaoQuestaoController : ApiController
    {
        private readonly Resolucoes _resolucoes;

        public ResolucaoQuestaoController(): this(new Resolucoes(NhibernateSetup.GetSession())) { }

        public ResolucaoQuestaoController(Resolucoes resolucoes) {
            _resolucoes = resolucoes;
        }

        [HttpPost]
        public IHttpActionResult Salvar(Interface.Resolucao resolucao)
        {
            var resolucaoMapeada = Mapper.Map<List<ResolucaoQuestao>>(resolucao);

            _resolucoes.SalvarResolucaoQuestao(resolucaoMapeada);

            return Ok();
        }

    }
}

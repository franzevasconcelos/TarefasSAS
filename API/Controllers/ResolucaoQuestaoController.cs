using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IHttpActionResult ObterResolucaoAluno(int idAluno, int idTarefa) {

            var resolucao = _resolucoes.ResolucaoQuestaoPorTarefaEAluno(idTarefa, idAluno);

            var resolucaoMapeada = Mapper.Map<Interface.Resolucao>(resolucao);
            return Ok(resolucaoMapeada);
        }

        [HttpPut]
        public IHttpActionResult SalvarResolucaoAluno(Interface.Resolucao resolucao) {
            var listaResolucaoQuestao = _resolucoes.ResolucaoQuestaoPorTarefaEAluno(resolucao.IdTarefa, resolucao.IdAluno);

            foreach (var resolucaoQuestao in listaResolucaoQuestao) {
                resolucaoQuestao.Comentario = resolucao.Questoes.First(q => q.Id == resolucaoQuestao.Questao.Id).Comentario;
                _resolucoes.SalvarResolucaoQuestao(resolucaoQuestao);
            }

            var resolucaoTarefa = _resolucoes.ResolucaoTarefaPorTarefaEAluno(resolucao.IdAluno, resolucao.IdTarefa);
            resolucaoTarefa.Nota = resolucao.Nota;
            _resolucoes.SalvarResolucaoTarefa(resolucaoTarefa);

            return Ok();
        }

    }
}

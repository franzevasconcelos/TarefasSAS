using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Controllers {
    public class TarefasController : ApiController {
        private readonly Tarefas _tarefas;
        private readonly Resolucoes _resolucoes;

        TarefasController() : this(new Tarefas(NhibernateSetup.GetSession()),
                                   new Resolucoes(NhibernateSetup.GetSession())) { }

        TarefasController(Tarefas tarefas, Resolucoes resolucoes) {
            _tarefas = tarefas;
            _resolucoes = resolucoes;
        }

        public IHttpActionResult Salvar(Interface.Tarefa tarefa) {
            var tarefaMapeada = Mapper.Map<Tarefa>(tarefa);
            _tarefas.Salvar(tarefaMapeada);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Listar(int idProfessor) {
            if (idProfessor <= 0) {
                return BadRequest("É necessário informar um professor.");
            }

            var tarefas = _tarefas.PorProfessor(idProfessor);

            if (tarefas == null)
                return NotFound();

            var tarefasMapeadas = Mapper.Map<List<Interface.Tarefa>>(tarefas);

            return Ok(tarefasMapeadas);
        }

        [HttpGet]
        public IHttpActionResult AResolver(int idAluno) {
            if (idAluno <= 0) {
                return BadRequest("É necessário informar um professor.");
            }

            var resolucoesTarefasEncontradas = _resolucoes.PorAluno(idAluno);

            var resolucaoMapeada = Mapper.Map<List<Interface.Resolucao>>(resolucoesTarefasEncontradas);

            return Ok(resolucaoMapeada);
        }

        [HttpGet]
        public IHttpActionResult PorId(int idTarefa, int idAluno) {
            if (idTarefa <= 0)
            {
                return BadRequest("É necessário informar um professor.");
            }

            var tarefaEncontrada = _tarefas.Por(idTarefa);
            var listaResolucaoQuestao = _resolucoes.ResolucaoQuestaoPorTarefaEAluno(tarefaEncontrada.Id, idAluno);

            var resolucaoMapeada = Mapper.Map<Interface.Resolucao>(tarefaEncontrada);

            if (listaResolucaoQuestao.Any()) {
                foreach (var item in resolucaoMapeada.Questoes) {
                    item.Cometario = listaResolucaoQuestao.FirstOrDefault(l => l.Questao.Id == item.Id)?.Comentario;
                    item.Resposta= listaResolucaoQuestao.FirstOrDefault(l => l.Questao.Id == item.Id)?.Resposta;
                }
            }


            return Ok(resolucaoMapeada);
        }

    }
}
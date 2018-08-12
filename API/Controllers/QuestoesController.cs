using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Controllers {
    public class QuestoesController : ApiController {
        private readonly Questoes _questoes;
        private readonly Tarefas _tarefas;
        QuestoesController() : this(new Questoes(NhibernateSetup.GetSession()), 
                                    new Tarefas(NhibernateSetup.GetSession())) { }

        public QuestoesController(Questoes questoes, Tarefas tarefas) {
            _questoes = questoes;
            _tarefas = tarefas;
        }

        [HttpPost]
        public IHttpActionResult Salvar(Interface.Questao questao) {
            var questaoMapeada = Mapper.Map<Questao>(questao);
            _questoes.Salvar(questaoMapeada);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult PorProfessor(int idProfessor) {
            if (idProfessor <= 0) {
                return BadRequest("Informe um id de professor");
            }

            var questoesEncontradas = _questoes.PorProfessor(idProfessor);

            if (!questoesEncontradas.Any()) {
                return NotFound();
            }

            var questoesMapeadas = Mapper.Map<IList<Interface.Questao>>(questoesEncontradas);

            return Ok(questoesMapeadas);
        }

        [HttpGet]
        public IHttpActionResult PorTarefa(int idTarefa) {
            if (idTarefa <= 0) {
                return BadRequest("Informe um id da tarefa");
            }

            var questoesEncontradas = _tarefas.QuestoesPorTarefa(idTarefa);

            if (!questoesEncontradas.Any()) {
                return NotFound();
            }

            var questoesMapeadas = Mapper.Map<IList<Interface.Questao>>(questoesEncontradas);

            return Ok(questoesMapeadas);
        }
    }
}
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Controllers {
    public class TurmaController : ApiController {
        private readonly Turmas _turmas;
        private readonly Tarefas _tarefas;

        public TurmaController(Turmas turmas, Tarefas tarefas) {
            _turmas = turmas;
            _tarefas = tarefas;
        }

        public TurmaController() : this(new Turmas(NhibernateSetup.GetSession()), new Tarefas(NhibernateSetup.GetSession())) { }

        [HttpGet]
        public IHttpActionResult Listar(int idProfessor) {

            var turmasEncontradas = _turmas.PorProfessor(idProfessor);

            var turmasMapeadas = Mapper.Map<List<Interface.Turma>>(turmasEncontradas);
            return Ok(turmasMapeadas);
        }

        public IHttpActionResult SalvarTarefa(Interface.TarefaTurma tarefaTurma) {
            var turma = _turmas.Por(tarefaTurma.IdTurma);
            var tarefa = _tarefas.Por(tarefaTurma.IdTarefa);

            if (turma.Tarefas == null) {
                turma.Tarefas = new List<Tarefa>();
            }

            turma.Tarefas.Add(tarefa);
            tarefa.Turmas.Add(turma);
            
            _tarefas.Salvar(tarefa);

            return Ok();
        }
    }
}
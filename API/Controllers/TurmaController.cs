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
        private readonly Resolucoes _resolucoes;

        public TurmaController(Turmas turmas, Tarefas tarefas, Resolucoes resolucoes) {
            _turmas = turmas;
            _tarefas = tarefas;
            _resolucoes = resolucoes;
        }

        public TurmaController() : this(new Turmas(NhibernateSetup.GetSession()),
                                        new Tarefas(NhibernateSetup.GetSession()),
                                        new Resolucoes(NhibernateSetup.GetSession())) { }

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

            foreach (var aluno in turma.Alunos) {
                _resolucoes.SalvarResolucaoTarefa(new ResolucaoTarefa {
                                                                          Aluno = aluno,
                                                                          Tarefa = tarefa,
                                                                          Enviada = false
                                                                      });
            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult ObterAlunos(int idTurma, int idTarefa) {
            var alunos = _turmas.ObterAlunos(idTurma);
            var alunosMapeados = Mapper.Map<List<Interface.AlunoTarefa>>(alunos);

            foreach (var item in alunosMapeados) {
                var resolucao = _resolucoes.ResolucaoTarefaPorTarefaEAluno(item.Id, idTarefa);
                item.TarefaResolvida = resolucao.Enviada;
                item.IdTarefa = idTarefa;
            }

            return Ok(alunosMapeados);
        }
    }
}
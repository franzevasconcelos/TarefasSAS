using System;
using System.Collections.Generic;
using NHibernate;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Persistencia {
    public class Resolucoes {
        private readonly ISession _session;

        public Resolucoes(ISession session) {
            _session = session;
        }

        public virtual IList<ResolucaoTarefa> PorAluno(int idAluno) {
            Aluno aluno = null;
            return _session.QueryOver<ResolucaoTarefa>()
                           .JoinAlias(t => t.Aluno, () => aluno)
                           .Where(t => aluno.Id == idAluno)
                           .List<ResolucaoTarefa>();
        }

        public virtual void SalvarResolucaoTarefa(ResolucaoTarefa resolucaoTarefa) {
            _session.SaveOrUpdate(resolucaoTarefa);
        }

        public virtual void SalvarResolucaoQuestao(List<ResolucaoQuestao> resolucaoMapeada) {
            foreach (var item in resolucaoMapeada) {
                _session.SaveOrUpdate(item);
            }
        }

        public virtual ResolucaoTarefa ResolucaoTarefaPorId(int id) {
            return _session.Get<ResolucaoTarefa>(id);
        }

        public virtual IList<ResolucaoQuestao> ResolucaoQuestaoPorTarefaEAluno(int idTarefa, int idAluno) {
            Tarefa tarefa = null;
            Aluno aluno = null;
            return _session.QueryOver<ResolucaoQuestao>()
                           .JoinAlias(r => r.Tarefa, () => tarefa)
                           .JoinAlias(r => r.Aluno, () => aluno)
                           .Where(r => tarefa.Id == idTarefa)
                           .And(r => aluno.Id == idAluno)
                           .List();
        }

        public ResolucaoTarefa ResolucaoTarefaPorTarefaEAluno(int idAluno, int idTarefa) {
            Tarefa tarefa = null;
            Aluno aluno = null;

            return _session.QueryOver<ResolucaoTarefa>()
                           .JoinAlias(r => r.Aluno, () => aluno)
                           .JoinAlias(r => r.Tarefa, () => tarefa)
                           .Where(() => aluno.Id == idAluno)
                           .And(() => tarefa.Id == idTarefa)
                           .SingleOrDefault();
        }
    }
}
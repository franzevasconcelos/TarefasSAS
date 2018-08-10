using System.Collections.Generic;
using NHibernate;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Persistencia {
    public class Tarefas {
        private readonly ISession _session;

        public Tarefas(ISession session) {
            _session = session;
        }

        public virtual IList<Tarefa> Por(int idProfessor) {
            return _session.QueryOver<Tarefa>()
                           .Where(t => t.Professor.Id == idProfessor)
                           .List();
        }

        public virtual IList<Questao> QuestoesPorTarefa(int idTarefa) {
            Tarefa tarefa = null;

            return _session.QueryOver<Questao>()
                           .JoinAlias(q => q.Tarefas, () => tarefa)
                           .Where(q => tarefa.Id == idTarefa)
                           .List();
        }

        public virtual void Salvar(Tarefa tarefa) {
            _session.SaveOrUpdate(tarefa);
        }
    }
}
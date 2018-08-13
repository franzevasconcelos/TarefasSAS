using System.Collections.Generic;
using NHibernate;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Persistencia {
    public class Turmas {
        private readonly ISession _session;

        public Turmas(ISession session) {
            _session = session;
        }

        public virtual IList<Turma> PorProfessor(int idProfessor) {
            return _session.QueryOver<Turma>()
                           .Where(t => t.Professor.Id == idProfessor)
                           .List();
        }

        public virtual Turma Por(int id) {
            return _session.QueryOver<Turma>().Where(t => t.Id == id).SingleOrDefault();
        }

        public void Salvar(Turma turma) {
            _session.SaveOrUpdate(turma);
        }

        public virtual IList<Aluno> ObterAlunos(int idTurma) {
            Turma turma = null;
            return _session.QueryOver<Aluno>()
                           .JoinAlias(t => t.Turma, () => turma)
                           .Where(() => turma.Id == idTurma)
                           .List();
        }
    }
}
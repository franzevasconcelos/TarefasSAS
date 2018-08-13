using NHibernate;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Persistencia {
    public class Alunos {
        private readonly ISession _session;

        public Alunos(ISession session) {
            _session = session;
        }

        public virtual Aluno Por(int id) {
            return _session.Get<Aluno>(id);
        }
    }
}
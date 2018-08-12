using System.Collections.Generic;
using NHibernate;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Persistencia {
    public class Professores {
        private readonly ISession _session;

        public Professores(ISession session) {
            _session = session;
        }

        public IList<Professor> Todos() {
            return _session.QueryOver<Professor>().List();
        }

        public virtual Professor Por(int id) {
            return _session.Get<Professor>(id);
        }
    }
}
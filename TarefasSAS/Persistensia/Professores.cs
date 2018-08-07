using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Persistensia {
    public class Professores {
        private readonly ISession _session;

        public Professores(ISession session) {
            _session = session;
        }

        public IList<Professor> Todos() {
            return _session.QueryOver<Professor>().List();
        }
    }
}
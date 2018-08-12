using System.Collections.Generic;
using System.Linq;
using NHibernate;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Persistencia {
    public class Questoes {
        private readonly ISession _sessao;

        public Questoes(ISession sessao) {
            _sessao = sessao;
        }

        public virtual void Salvar(Questao questao) {
            _sessao.SaveOrUpdate(questao);
        }

        public virtual Questao Por(int id) {
            return _sessao.Query<Questao>().Where(q => q.Id == id).SingleOrDefault();
        }

        public virtual IList<Questao> PorProfessor(int idProfessor) {
            return _sessao.QueryOver<Questao>()
                          .Where(q => q.Professor.Id == idProfessor)
                          .List();
        }
    }
}
using System.Collections.Generic;
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

        public virtual IList<Questao> Por(int idProfessor) {
            return _sessao.QueryOver<Questao>()
                          .Where(q => q.Professor.Id == idProfessor)
                          .List();
        }
    }
}
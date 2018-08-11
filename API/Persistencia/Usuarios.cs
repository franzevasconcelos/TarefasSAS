using NHibernate;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Models;

namespace TarefasSAS.API.Persistencia {
    public class Usuarios {
        private readonly ISession _session;

        public Usuarios(ISession session) {
            this._session = session;
        }

        public TipoUsuario ObterUsuario(string login) {
            Usuario usuario1 = null;
            var professor = _session.QueryOver<Professor>()
                                    .JoinAlias(p => p.Usuario, () => usuario1)
                                    .Where(() => usuario1.Login == login)
                                    .SingleOrDefault();


            if (professor != null)
                return new TipoUsuario {
                                           Id = professor.Id,
                                           Nome = professor.Nome,
                                           TipoLogin = TipoLogin.Professor
                                       };

            Usuario usuario2 = null;
            var aluno = _session.QueryOver<Aluno>()
                                .JoinAlias(p => p.Usuario, () => usuario2)
                                .Where(() => usuario2.Login == login)
                                .SingleOrDefault();

            if (aluno != null) {
                return new TipoUsuario {
                                           Id = aluno.Id,
                                           Nome = aluno.Nome,
                                           TipoLogin = TipoLogin.Aluno
                                       };
            }

            return null;
        }
    }
}
using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class AlunoMap : ClassMap<Aluno> {
        private AlunoMap() {
            Id(a => a.Id);
            Map(a => a.Nome);
            Map(a => a.Email).Nullable();
            Map(a => a.Nascimento).Nullable();
            References(a => a.Turma).Nullable();
            References(a => a.Usuario);
        }
    }
}
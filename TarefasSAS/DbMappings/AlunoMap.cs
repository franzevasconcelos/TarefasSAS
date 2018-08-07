using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class AlunoMap : ClassMap<Aluno> {
        private AlunoMap() {
            Id(a => a.Id);
            Map(a => a.Nome);
            References(a => a.Turma);
        }
    }
}
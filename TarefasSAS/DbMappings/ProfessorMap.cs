using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class ProfessorMap : ClassMap<Professor> {
        ProfessorMap() {
            Id(p => p.Id);
            Map(p => p.Nome);
            HasOne(p => p.Usuario);
        }
    }
}
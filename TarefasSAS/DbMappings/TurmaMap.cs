using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class TurmaMap : ClassMap<Turma> {
        TurmaMap() {
            Id(t => t.Id);
            //Map(t => t.Nome);
            HasOne(t => t.Professor);
            HasMany(t => t.Alunos).Cascade.All();
        }
    }
}
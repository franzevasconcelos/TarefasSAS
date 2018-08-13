using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class TurmaMap : ClassMap<Turma> {
        TurmaMap() {
            Id(t => t.Id);
            Map(t => t.Nome);
            References(t => t.Professor);
            HasMany(t => t.Alunos).Cascade.All();
            HasManyToMany(t => t.Tarefas)
                .Cascade
                .All()
                .Inverse()
                .Table("TarefaTurma");
        }
    }
}
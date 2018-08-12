using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class TarefaMap : ClassMap<Tarefa> {
        TarefaMap() {
            Id(q => q.Id);
            References(t => t.Professor);
            HasManyToMany(t => t.Turmas)
                .Cascade
                .All()
                .Table("TarefaTurma");
            HasManyToMany(t => t.Questoes)
                .Cascade
                .All()
                .Table("TarefaQuestao");
        }
    }
}
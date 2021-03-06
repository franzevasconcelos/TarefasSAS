﻿using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class QuestaoMap : ClassMap<Questao> {
        QuestaoMap() {
            Id(q => q.Id);
            Map(q => q.Pergunta);
            References(q => q.Professor);
            HasManyToMany(q => q.Tarefas)
                .Cascade.All()
                .Inverse()
                .Table("TarefaQuestao");
        }
    }
}
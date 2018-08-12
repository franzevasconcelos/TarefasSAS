using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class ResolucaoQuestaoMap : ClassMap<ResolucaoQuestao> {
        ResolucaoQuestaoMap() {
            Id(r => r.Id);
            Map(q => q.Comentario).Nullable();
            Map(q => q.Resposta);
            References(q => q.Aluno);
            References(q => q.Questao);
            References(q => q.Tarefa);
        }
    }
}
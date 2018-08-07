using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class ResolucaoMap : ClassMap<Resolucao> {
        ResolucaoMap() {
            Id(r => r.Id);
            Map(q => q.Comentario);
            Map(q => q.Nota);
            Map(q => q.Enviada);
            Map(q => q.Resposta);
            References(q => q.Aluno);
            References(q => q.Questao);
            References(q => q.Tarefa);
        }
    }
}
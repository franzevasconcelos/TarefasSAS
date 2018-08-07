using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings
{
    public class QuestaoMap : ClassMap<Questao>
    {
        QuestaoMap()
        {
            Id(q => q.Id);
            Map(q => q.Pergunta);
            Map(q => q.Resposta);
        }
    }
}
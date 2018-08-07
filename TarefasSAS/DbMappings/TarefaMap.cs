using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings
{
    public class TarefaMap : ClassMap<Tarefa>
    {
        TarefaMap()
        {
            Id(q => q.Id);
            HasMany(t => t.Questoes);
            
        }
    }
}
using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings {
    public class ResolucaoTarefaMap : ClassMap<ResolucaoTarefa> {
        public ResolucaoTarefaMap() {
            Id(r => r.Id);
            Map(r => r.Nota).Nullable();
            Map(r => r.Enviada).Nullable();
            References(r => r.Tarefa);
            References(r => r.Aluno);
        }
    }
}
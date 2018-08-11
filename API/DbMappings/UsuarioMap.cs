using FluentNHibernate.Mapping;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.DbMappings
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Id(u => u.Id);
            Map(u => u.Login);
        }
    }
}
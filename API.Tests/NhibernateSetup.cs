using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TarefasSAS.API.DbMappings;

namespace API.Tests {
    public class NhibernateSetup {
        public Configuration Getconfiguation() {
            return Fluently.Configure()
                           .Database(SQLiteConfiguration.Standard.InMemory)
                           .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(AlunoMap).Assembly))
                           .BuildConfiguration();
        }

        public ISessionFactory Createsessionfactory() {
            var configuration = Getconfiguation();
            
            var sessionfactory = Fluently.Configure()
                                         .Database(SQLiteConfiguration.Standard.InMemory)
                                         .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(AlunoMap)
                                                                                             .Assembly))
                                         .ExposeConfiguration(cfg => {
                                             cfg.SetProperty("show_sql", "True");
                                             cfg.SetProperty("format_sql", "True");
                                             configuration = cfg;
                                         })
                                         .BuildSessionFactory();


            return sessionfactory;
        }
        
        public void BuildSchema(ISession session) {
            var export = new SchemaExport(Getconfiguation());
            export.Execute(true, true, false, session.Connection, null);
        }
    }
}
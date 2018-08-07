using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using TarefasSAS.API.DbMappings;

namespace TarefasSAS.API.Configuracoes {
    public static class NhibernateSetup {
        public static ISessionFactory SessionFactory;
        private const string ArquivoSqlite = @"D:\Desenvolvimento\SQLite\tarefas_sas.db";

        public static void Init() {
            SessionFactory = Fluently.Configure()
                                     .Database(SQLiteConfiguration
                                               .Standard
                                               .UsingFile(ArquivoSqlite)
                                               .ShowSql())
                                     .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(AlunoMap).Assembly))
                                     .ExposeConfiguration(config => {
                                         config.SetProperty("current_session_context_class", "web");
                                         //config.SetProperty("hbm2ddl.auto", "validate");
                                     })
                                     .BuildSessionFactory();
        }

        public static ISession GetSession() {
            if (!SessionFactory.IsClosed)
                return SessionFactory.GetCurrentSession();

            return SessionFactory.OpenSession();
        }
    }
}
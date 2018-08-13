﻿using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using TarefasSAS.API.DbMappings;

namespace TarefasSAS.API.Configuracoes {
    public static class NhibernateSetup {
        public static ISessionFactory SessionFactory;
        private static readonly string ArquivoSqlite = new AppSettingsReader().GetValue("Banco.Arquivo", typeof(string)).ToString();

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
using System.IO;
using NDbUnit.Core;
using NDbUnit.Core.SqlLite;
using NHibernate;
using NUnit.Framework;

namespace API.Tests.Persistencia {
    public abstract class PersistenciaBaseTest {
        protected SqlLiteDbUnitTest Banco;
        protected ISession Sessao;
        protected NhibernateSetup NhSetup;

        protected abstract string NomeXmlDataset { get; }

        [OneTimeSetUp]
        public void TestFixtureSetup() {
            NhSetup = new NhibernateSetup();
        }

        [OneTimeTearDown]
        public void TearDown() {
            Banco.PerformDbOperation(DbOperationFlag.DeleteAll);
            Sessao.Close();
            Sessao.Dispose();
        }

        [SetUp]
        public void Setup() {
            var caminhoProjeto = TestContext.CurrentContext.TestDirectory.Replace(@"bin\Debug", string.Empty)
                                            .Replace(@"bin\Release", string.Empty);

            Sessao = NhSetup.Createsessionfactory().OpenSession();
            NhSetup.BuildSchema(Sessao);

            Banco = new SqlLiteDbUnitTest(Sessao.Connection);

            Banco.ReadXmlSchema(Path.Combine(caminhoProjeto, @"Persistencia\Datasets\banco_dataset.xsd"));
            Banco.ReadXml(Path.Combine(caminhoProjeto, $@"Persistencia\Datasets\{NomeXmlDataset}"));


            Banco.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);
        }
    }
}
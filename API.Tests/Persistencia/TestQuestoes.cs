using System.IO;
using NDbUnit.Core.SqlLite;
using NHibernate;
using NUnit.Framework;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Persistencia {
    [TestFixture]
    public class TestQuestoes {
        SqlLiteDbUnitTest _banco;
        private ISession sessao;

        [OneTimeSetUp]
        public void TestFixtureSetup() {
            var caminhoProjeto = TestContext.CurrentContext.TestDirectory.Replace(@"bin\Debug", string.Empty).Replace(@"bin\Release", string.Empty);
            var caminhoBanco = Path.Combine(caminhoProjeto, @"Persistencia\Datasets\banco_teste.db");
            var connectionString = $@"Data Source={caminhoBanco};Version=3;";

            _banco = new SqlLiteDbUnitTest(connectionString);

            _banco.ReadXmlSchema(Path.Combine(caminhoProjeto, @"Persistencia\Datasets\banco_dataset.xsd"));
            _banco.ReadXml(Path.Combine(caminhoProjeto, @"Persistencia\Datasets\questoes.xml"));

            NhibernateSetup.Init();
            sessao = NhibernateSetup.SessionFactory.OpenSession();
        }

        [OneTimeTearDown]
        public void TearDown() {
            _banco.PerformDbOperation(NDbUnit.Core.DbOperationFlag.DeleteAll);
            sessao.Close();
            sessao.Dispose();
        }

        [SetUp]
        public void Setup() {
            _banco.PerformDbOperation(NDbUnit.Core.DbOperationFlag.CleanInsertIdentity);
        }

        [Test]
        public void DeveSalvarQuestao() {
            var questoes = new Questoes(sessao);
            questoes.Salvar(new Questao {
                                            Professor = new Professor {Id = 1},
                                            Pergunta = "Um segunda pergunta"
                                        });

            var questaos = sessao.QueryOver<Questao>().List();

            var lista = questoes.Por(1);

            Assert.That(lista.Count, Is.EqualTo(2));
        }
    }
}
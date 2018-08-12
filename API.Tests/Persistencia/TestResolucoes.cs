using NUnit.Framework;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Persistencia
{
    [TestFixture]
    public class TestResolucoes:PersistenciaBaseTest {
        protected override string NomeXmlDataset => "resolucoes.xml";

        [Test]
        public void DeveTrazerResolucoesTarefaPorAluno() {
            var resolucoes = new Resolucoes(Sessao);

            var tarefas = resolucoes.PorAluno(1);

            Assert.That(tarefas.Count, Is.EqualTo(1));
        }
    }
}

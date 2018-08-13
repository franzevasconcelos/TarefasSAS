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

        [Test]
        public void DeveTrazerResolucaTarefaPorAlunoETarefa() {
            var resolucoes = new Resolucoes(Sessao);

            var resolucaoTarefa = resolucoes.ResolucaoTarefaPorTarefaEAluno(1, 3);

            Assert.That(resolucaoTarefa, Is.Not.Null);
        }

        [Test]
        public void DeveTrazerResolucaoQuestaoPorAlunoETarefa() {
            var resolucoes = new Resolucoes(Sessao);

            var listaEncontrada = resolucoes.ResolucaoQuestaoPorTarefaEAluno(1, 1);

            Assert.That(listaEncontrada.Count, Is.EqualTo(1));
        }
    }
}

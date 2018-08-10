using NUnit.Framework;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Persistencia {
    [TestFixture]
    class TestTarefas : PersistenciaBaseTest {
        protected override string NomeXmlDataset => "tarefas.xml";

        [Test]
        public void DeveTrazerTodasAsTarefasDeUmProfessorCorretamente() {
            var tarefas = new Tarefas(Sessao);

            var tarefasEncontradas = tarefas.Por(1);

            Assert.That(tarefasEncontradas.Count, Is.EqualTo(3));
            Assert.That(tarefasEncontradas[0].Id, Is.EqualTo(1));
            Assert.That(tarefasEncontradas[1].Id, Is.EqualTo(2));
            Assert.That(tarefasEncontradas[2].Id, Is.EqualTo(3));
        }

        [Test]
        public void DeveTrazerTodasAsQuestoesDeUmaTarefa() {
            var tarefas = new Tarefas(Sessao);

            var questoresEncontradas = tarefas.QuestoesPorTarefa(3);

            Assert.That(questoresEncontradas.Count, Is.EqualTo(2));
            Assert.That(questoresEncontradas[0].Id, Is.EqualTo(3));
            Assert.That(questoresEncontradas[1].Id, Is.EqualTo(4));
        }
    }
}
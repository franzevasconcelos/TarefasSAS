using NUnit.Framework;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Persistencia {
    [TestFixture]
    public class TestQuestoes : PersistenciaBaseTest {
        protected override string NomeXmlDataset => "questoes.xml";

        [Test]
        public void DeveTrazerAsQuestoesDoProfessorCorretamente() {
            var questoes = new Questoes(Sessao);

            var questoesEncontradas = questoes.Por(idProfessor: 2);

            Assert.That(questoesEncontradas.Count, Is.EqualTo(2));
            Assert.That(questoesEncontradas[0].Id, Is.EqualTo(2));
            Assert.That(questoesEncontradas[1].Id, Is.EqualTo(3));
        }

        [Test]
        public void DeveSalvarQuestao() {
            var prof = Sessao.Get<Professor>(1);

            var questoes = new Questoes(Sessao);
            questoes.Salvar(new Questao {
                                            Professor = prof,
                                            Pergunta = "Um nova pergunta"
                                        });

            var questoesEncontradas = questoes.Por(1);

            Assert.That(questoesEncontradas.Count, Is.EqualTo(2));
        }
    }
}
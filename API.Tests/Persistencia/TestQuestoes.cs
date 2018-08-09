using NUnit.Framework;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Persistencia {
    [TestFixture]
    public class TestQuestoes : PersistenciaBaseTest {
        protected override string NomeXmlDataset => "questoes.xml";

        [Test]
        public void DeveSalvarQuestao() {
            var prof = Sessao.Get<Professor>(1);

            var questoes = new Questoes(Sessao);
            questoes.Salvar(new Questao {
                                            Professor = prof,
                                            Pergunta = "Um segunda pergunta"
                                        });

            var lista = questoes.Por(1);

            Assert.That(lista.Count, Is.EqualTo(2));
        }
    }
}
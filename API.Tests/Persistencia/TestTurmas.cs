using NUnit.Framework;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Persistencia
{
    [TestFixture]
    public class TestTurmas:PersistenciaBaseTest {
        protected override string NomeXmlDataset => "turmas.xml";

        [Test]
        public void DeveTrazerAlunosAoPerquisarPorTurma() {
            var turmas = new Turmas(Sessao);

            var alunos = turmas.ObterAlunos(1);

            Assert.That(alunos.Count, Is.EqualTo(2));
        }

        [Test]
        public void DeveTrazerTurmasDoProfessor() {
            var turmas = new Turmas(Sessao);

            var turmasEncontradas = turmas.PorProfessor(1);

            Assert.That(turmasEncontradas.Count, Is.EqualTo(1));
        }
    }
}

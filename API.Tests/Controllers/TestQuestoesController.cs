using System.Collections.Generic;
using System.Web.Http.Results;
using NHibernate;
using NSubstitute;
using NUnit.Framework;
using TarefasSAS.API.Controllers;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Controllers {
    [TestFixture]
    public class TestQuestoesController {
        [Test]
        public void DeveDevolverBadRequestAoBuscarQuestoesComUmIdInvalidoDeProfessor() {
            var controller = new QuestoesController(null, null, null);

            var retorno = controller.PorProfessor(0);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
            Assert.That(((BadRequestErrorMessageResult) retorno).Message, Is.EqualTo("Informe um id de professor"));
        }

        [Test]
        public void DeveDevolverNotFoundAoNaoEncontrarQuestoesDoProfessor() {
            var questoes = Substitute.For<Questoes>((ISession) null);
            questoes.PorProfessor(1).Returns(new List<Questao>());

            var controller = new QuestoesController(questoes, null, null);

            var retorno = controller.PorProfessor(1);

            Assert.IsInstanceOf<NotFoundResult>(retorno);
        }

        [Test]
        public void DeveDevolverBadRequestAoBuscarQuestoesComUmIdInvalidoDeUmaTarefa() {
            var controller = new QuestoesController(null, null, null);

            var retorno = controller.PorTarefa(0);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
            Assert.That(((BadRequestErrorMessageResult) retorno).Message, Is.EqualTo("Informe um id da tarefa"));
        }

        [Test]
        public void DeveDevolverNotFoundAoNaoEncontrarQuestoesDaTarefa() {
            var tarefas = Substitute.For<Tarefas>((ISession) null);
            tarefas.QuestoesPorTarefa(1).Returns(new List<Questao>());

            var controller = new QuestoesController(null, tarefas, null);

            var retorno = controller.PorTarefa(1);

            Assert.IsInstanceOf<NotFoundResult>(retorno);
        }
    }
}
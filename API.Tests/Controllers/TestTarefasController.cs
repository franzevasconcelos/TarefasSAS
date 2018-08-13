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
    class TestTarefasController {
        [Test]
        public void DeveDevolverBadRequestAoListarTarefasComUmIdInvalidoDeProfessor() {
            var controller = new TarefasController(null, null, null);

            var retorno = controller.Listar(0);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
            Assert.That(((BadRequestErrorMessageResult) retorno).Message,
                        Is.EqualTo("É necessário informar um professor."));
        }

        [Test]
        public void DeveDevolverNotFoundAoListarTarefasComUmIdInexistenteDeProfessor() {
            var tarefas = Substitute.For<Tarefas>((ISession) null);
            tarefas.PorProfessor(1).Returns(new List<Tarefa>());

            var controller = new TarefasController(tarefas, null, null);

            var retorno = controller.Listar(1);

            Assert.IsInstanceOf<NotFoundResult>(retorno);
        }

        [Test]
        public void DeveDevolverBadRequestAoListarTarefasComUmIdInvalidoDeAluno() {
            var controller = new TarefasController(null, null, null);

            var retorno = controller.AResolver(0);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
            Assert.That(((BadRequestErrorMessageResult) retorno).Message,
                        Is.EqualTo("É necessário informar um aluno."));
        }

        [Test]
        public void DeveDevolverBadRequestAoListarTarefasComUmIdInvalidoDeAlunoOuTarefa() {
            var controller = new TarefasController(null, null, null);

            var retorno = controller.PorId(0, 0);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
            Assert.That(((BadRequestErrorMessageResult) retorno).Message,
                        Is.EqualTo("É necessário informar o aluno e a tarefa."));
        }
    }
}
using System.Web.Http.Results;
using NHibernate;
using NSubstitute;
using NUnit.Framework;
using TarefasSAS.API.Controllers;
using TarefasSAS.API.Models;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Controllers
{
    [TestFixture]
    public class TestUsuarioController
    {
        [Test]
        public void DeveRetornarBadRequestAoTentarLogarPassarLoginNulo() {
            var usuarios = Substitute.For<Usuarios>((ISession)null);

            var controller = new UsuarioController(usuarios);

            var retorno = controller.Index(null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
            Assert.That(((BadRequestErrorMessageResult) retorno).Message, Is.EqualTo("Informe o login"));
        }

        [Test]
        public void DeveOkAoTentarLogarComLoginValido()
        {
            var usuarios = Substitute.For<Usuarios>((ISession)null);
            usuarios.ObterUsuario("professor").Returns(new TipoUsuario {Id = 1, TipoLogin = TipoLogin.Professor});

            var controller = new UsuarioController(usuarios);

            var retorno = controller.Index("professor");

            Assert.IsInstanceOf<OkNegotiatedContentResult<TipoUsuario>>(retorno);
            Assert.That(((OkNegotiatedContentResult<TipoUsuario>)retorno).Content.TipoLogin, Is.EqualTo(TipoLogin.Professor));
        }
        
        [Test]
        public void DeveRetornarNotFoundAoTentarLogarComLoginInexistente()
        {
            var usuarios = Substitute.For<Usuarios>((ISession)null);
            usuarios.ObterUsuario(Arg.Any<string>()).Returns((TipoUsuario)null);

            var controller = new UsuarioController(usuarios);

            var retorno = controller.Index("fantasma");

            Assert.IsInstanceOf<NotFoundResult>(retorno);
        }

    }
}

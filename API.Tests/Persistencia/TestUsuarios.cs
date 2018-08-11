using NUnit.Framework;
using TarefasSAS.API.Models;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Persistencia {
    [TestFixture]
    public class TestUsuarios : PersistenciaBaseTest {
        protected override string NomeXmlDataset => "usuarios.xml";

        [Test]
        public void DeveRetornarProfessorAoBuscarPorLogin() {
            var usuarios = new Usuarios(Sessao);

            var retorno = usuarios.ObterUsuario("professor");

            Assert.That(retorno, Is.Not.Null);
            Assert.That(retorno.TipoLogin, Is.EqualTo(TipoLogin.Professor));
            Assert.That(retorno.Nome, Is.EqualTo("Professor Fulano"));
        }

        [Test]
        public void DeveRetornarAlunoAoBuscarPorLogin() {
            var usuarios = new Usuarios(Sessao);

            var retorno = usuarios.ObterUsuario("aluno");

            Assert.That(retorno, Is.Not.Null);
            Assert.That(retorno.TipoLogin, Is.EqualTo(TipoLogin.Aluno));
            Assert.That(retorno.Nome, Is.EqualTo("Aluno Beltrano"));
        }

        [Test]
        public void DeveRetornarNuloAoBuscarPorLoginInexistente() {
            var usuarios = new Usuarios(Sessao);

            var retorno = usuarios.ObterUsuario("inexistente");

            Assert.That(retorno, Is.Null);
        }
    }
}
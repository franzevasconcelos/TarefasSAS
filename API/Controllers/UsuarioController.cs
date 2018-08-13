using System.Web.Http;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Controllers {
    public class UsuarioController : ApiController {
        private readonly Usuarios _usuarios;

        public UsuarioController(Usuarios usuarios) {
            _usuarios = usuarios;
        }

        public UsuarioController() : this(new Usuarios(NhibernateSetup.GetSession())) { }

        [HttpGet]
        public IHttpActionResult Index(string login) {

            if (string.IsNullOrEmpty(login))
                return BadRequest("Informe o login");

            var retorno = _usuarios.ObterUsuario(login);

            if(retorno == null)
                return NotFound();

            return Ok(retorno);
        }
    }
}
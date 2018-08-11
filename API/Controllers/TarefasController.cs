using System.Web.Http;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Controllers
{
    public class TarefasController : ApiController
    {
        private readonly Tarefas _tarefas;

        TarefasController() : this(new Tarefas(NhibernateSetup.GetSession())) { }

        TarefasController(Tarefas tarefas) {
            _tarefas = tarefas;
        }

        [HttpGet]
        public IHttpActionResult Listar(int idProfessor) {
            if (idProfessor <= 0) {
                return BadRequest("É necessário informar um professor.");
            }

            var tarefas = _tarefas.Por(idProfessor);

            if (tarefas == null)
                return NotFound();

            return Ok(tarefas);
        }
    }
}

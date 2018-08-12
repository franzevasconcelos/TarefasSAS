using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
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

        public IHttpActionResult Salvar(Interface.Tarefa tarefa) {
            var tarefaMapeada = Mapper.Map<Tarefa>(tarefa);
            _tarefas.Salvar(tarefaMapeada);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Listar(int idProfessor) {
            if (idProfessor <= 0) {
                return BadRequest("É necessário informar um professor.");
            }

            var tarefas = _tarefas.PorProfessor(idProfessor);

            if (tarefas == null)
                return NotFound();

            var tarefasMapeadas = Mapper.Map<List<Interface.Tarefa>>(tarefas);

            return Ok(tarefasMapeadas);
        }
    }
}

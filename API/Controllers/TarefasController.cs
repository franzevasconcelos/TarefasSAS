using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Controllers
{
    public class TarefasController : ApiController
    {
        private readonly Professores _professores;

        TarefasController() : this(new Professores(NhibernateSetup.GetSession())) { }

        TarefasController(Professores professores) {
            _professores = professores;
        }

        [HttpGet]
        public List<Professor> Boom() {
            
            return _professores.Todos().ToList();
        }
    }
}

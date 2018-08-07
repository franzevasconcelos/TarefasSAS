using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistensia;

namespace TarefasSAS.API.Controllers
{
    public class TesteController : ApiController
    {
        private readonly Professores _professores;

        TesteController() : this(new Professores(NhibernateSetup.GetSession())) { }

        TesteController(Professores professores) {
            _professores = professores;
        }

        [HttpGet]
        public List<Professor> Boom() {
            
            return _professores.Todos().ToList();
        }
    }
}

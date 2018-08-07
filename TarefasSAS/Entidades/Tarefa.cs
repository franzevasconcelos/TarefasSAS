using System.Collections.Generic;

namespace TarefasSAS.API.Entidades {
    public class Tarefa : Base {
        public virtual IList<Questao> Questoes { get; set; }
    }
}
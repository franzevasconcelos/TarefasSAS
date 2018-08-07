using System.Collections.Generic;

namespace TarefasSAS.API.Entidades {
    public class Tarefa : EntidadeBase {
        public virtual IList<Questao> Questoes { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual IList<Turma> Turmas { get; set; }
    }
}
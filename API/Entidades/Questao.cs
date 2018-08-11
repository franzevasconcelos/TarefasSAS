using System.Collections.Generic;

namespace TarefasSAS.API.Entidades {
    public class Questao : EntidadeBase {
        public virtual string Pergunta { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual IList<Tarefa> Tarefas { get; set; }
    }
}
using System.Collections.Generic;

namespace TarefasSAS.API.Entidades {
    public class Turma : EntidadeBase {
        public virtual string Nome { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual IList<Aluno> Alunos { get; set; }
        public virtual IList<Tarefa> Tarefas { get; set; }
    }
}
namespace TarefasSAS.API.Entidades {
    public class Aluno : EntidadeBase {
        public virtual string Nome { get; set; }
        public virtual Turma Turma { get; set; }
    }
}
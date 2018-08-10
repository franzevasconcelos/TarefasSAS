namespace TarefasSAS.API.Entidades {
    public class Aluno : EntidadeBase {
        public virtual string Nome { get; set; }
        public virtual Turma Turma { get; set; }
        public virtual string Email { get; set; }
        public virtual string Nascimento { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
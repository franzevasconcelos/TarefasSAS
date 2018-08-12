namespace TarefasSAS.API.Entidades {
    public class Professor : EntidadeBase {
        public virtual string Nome { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
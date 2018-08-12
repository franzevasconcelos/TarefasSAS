namespace TarefasSAS.API.Entidades {
    public class ResolucaoTarefa : EntidadeBase {
        public virtual Tarefa Tarefa { get; set; }
        public virtual Aluno Aluno { get; set; }
        public virtual double? Nota { get; set; }
        public virtual bool Enviada { get; set; }
    }
}
namespace TarefasSAS.API.Entidades {
    public class Resolucao : EntidadeBase {
        public virtual string Resposta { get; set; }
        public virtual string Comentario { get; set; }
        public virtual double Nota { get; set; }
        public virtual bool Enviada { get; set; }
        public virtual Aluno Aluno { get; set; }
        public virtual Tarefa Tarefa { get; set; }
        public virtual Questao Questao { get; set; }
    }
}
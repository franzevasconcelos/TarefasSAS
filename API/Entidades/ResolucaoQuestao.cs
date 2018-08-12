namespace TarefasSAS.API.Entidades {
    public class ResolucaoQuestao : EntidadeBase {
        public virtual string Resposta { get; set; }
        public virtual string Comentario { get; set; }
        public virtual Aluno Aluno { get; set; }
        public virtual Tarefa Tarefa { get; set; }
        public virtual Questao Questao { get; set; }
    }
}
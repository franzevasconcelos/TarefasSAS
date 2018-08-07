namespace TarefasSAS.API.Entidades {
    public class Questao : Base {
        public virtual string Pergunta { get; set; }
        public virtual string Resposta { get; set; }
    }
}
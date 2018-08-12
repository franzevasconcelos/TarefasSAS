namespace View.ViewModels {
    public class QuestaoViewModel {
        public int Id { get; set; }
        public int IdProfessor { get; set; }
        public string Pergunta { get; set; }
        public bool DeveSalvar { get; set; }
        public string Resposta { get; set; }
        public string Comentario { get; set; }
        public int IdAluno { get; set; }
    }
}
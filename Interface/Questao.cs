namespace Interface {
    public class Questao {
        public int Id { get; set; }
        public int IdProfessor { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public string Cometario { get; set; }
        public int IdAluno { get; set; }
    }
}
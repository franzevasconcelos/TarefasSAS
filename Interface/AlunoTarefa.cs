namespace Interface {
    public class AlunoTarefa {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdTurma { get; set; }
        public int IdTarefa { get; set; }
        public bool TarefaResolvida { get; set; }
    }
}
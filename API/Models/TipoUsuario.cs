namespace TarefasSAS.API.Models {
    public class TipoUsuario {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public TipoLogin TipoLogin { get; set; }
    }

    public enum TipoLogin {
        Professor,
        Aluno
    }
}
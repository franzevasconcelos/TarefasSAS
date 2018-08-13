using System.Collections.Generic;

namespace Interface {
    public class Tarefa {
        public IList<Questao> Questoes { get; set; }
        public int IdProfessor { get; set; }
        public int Id { get; set; }
    }
}
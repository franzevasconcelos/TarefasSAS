using System.Collections.Generic;

namespace Interface {
    public class Resolucao {
        public int Id { get; set; }
        public int IdTarefa { get; set; }
        public int IdAluno { get; set; }

        public List<Questao> Questoes { get; set; }
        public bool? Enviada { get; set; }
        public double? Nota { get; set; }
    }
}
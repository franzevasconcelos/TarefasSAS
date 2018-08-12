using System.Collections.Generic;

namespace View.ViewModels {
    public class TarefaViewModel {
        public int Id { get; set; }
        public List<QuestaoViewModel> Questoes { get; set; }

        public List<TurmaViewModel> Turmas { get; set; }

        public int TurmaEscolhida { get; set; }
    }
}
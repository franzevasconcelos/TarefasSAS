using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace View.ViewModels
{
    public class TarefaViewModel
    {
        public int Id { get; set; }
        public List<QuestaoViewModel> Questoes { get; set; }

        
    }
}
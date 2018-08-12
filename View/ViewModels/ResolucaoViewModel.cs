using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace View.ViewModels
{
    public class ResolucaoViewModel
    {
        public int Id { get; set; }
        public int IdTarefa { get; set; }
        public List<QuestaoViewModel> Questoes { get; set; }
        public double Nota { get; set; }
        public bool Enviada { get; set; }
    }
}
using AutoMapper;
using Interface;
using View.ViewModels;

namespace View.Mappers {
    public class QuestaoMapper : Profile {
        public QuestaoMapper() {
            CreateMap<QuestaoViewModel, Questao>().ReverseMap();
        }
    }
}
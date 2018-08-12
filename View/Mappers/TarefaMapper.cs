using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Interface;
using View.ViewModels;

namespace View.Mappers {
    public class TarefaMapper : Profile {
        public TarefaMapper() {
            CreateMap<List<Questao>, TarefaViewModel>()
                .ForMember(dest => dest.Questoes, opt => opt.MapFrom(src => src));

            CreateMap<TarefaViewModel, Tarefa>()
                .ForMember(dest => dest.Questoes, opt => opt.MapFrom(src => src.Questoes.Where(q => q.DeveSalvar)));
        }
    }
}
using AutoMapper;
using Interface;
using View.ViewModels;

namespace View.Mappers {
    public class TurmaMapper : Profile {
        public TurmaMapper() {
            CreateMap<Turma, TurmaViewModel>();

            CreateMap<TarefaViewModel, TarefaTurma>()
                .ForMember(dest => dest.IdTarefa, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdTurma, opt => opt.MapFrom(src => src.TurmaEscolhida));
        }
    }
}
using AutoMapper;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Mappers {
    public class TurmaMapper : Profile {
        public TurmaMapper() {
            CreateMap<Turma, Interface.Turma>()
                .ForMember(dest => dest.IdProfessor, opt => opt.MapFrom(src => src.Professor.Id));
        }
    }
}
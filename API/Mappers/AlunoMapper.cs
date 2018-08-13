using AutoMapper;
using TarefasSAS.API.Entidades;

namespace TarefasSAS.API.Mappers {
    public class AlunoMapper : Profile {
        public AlunoMapper() {
            CreateMap<Aluno, Interface.AlunoTarefa>()
                .ForMember(dest => dest.IdTurma, opt => opt.MapFrom(src => src.Turma.Id));
        }
    }
}
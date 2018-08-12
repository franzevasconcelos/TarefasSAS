using AutoMapper;
using View.ViewModels;

namespace View.Mappers {
    public class AlunoMapper : Profile {
        public AlunoMapper() {
            CreateMap<Interface.AlunoTarefa, AlunoTarefaViewModel>();
        }
    }
}
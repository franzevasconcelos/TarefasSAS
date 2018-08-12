using System.Collections.Generic;
using AutoMapper;
using View.ViewModels;

namespace View.Mappers {
    public class ResolucaoMapper : Profile {
        public ResolucaoMapper() {
            CreateMap<Interface.Resolucao, ResolucaoViewModel>();
            CreateMap<ResolucaoViewModel, Interface.Resolucao>()
                .ConstructUsing(ObterResolucao)
                .ForAllMembers(opt => opt.Ignore());
        }

        private Interface.Resolucao ObterResolucao(ResolucaoViewModel viewModel) {
            var resolucao = new Interface.Resolucao {
                                                        IdTarefa = viewModel.IdTarefa,
                                                        Questoes = new List<Interface.Questao>(),
                                                        Enviada = viewModel.Enviada,
                                                        Nota = viewModel.Nota
                                                    };


            foreach (var questao in viewModel.Questoes) {
                resolucao.Questoes.Add(new Interface.Questao {
                                                       IdAluno = questao.IdAluno,
                                                       Id = questao.Id,
                                                       Resposta = questao.Resposta,
                                                       Comentario = questao.Comentario,
                                                       IdProfessor = questao.IdProfessor
                                                   });
            }

            return resolucao;
        }
    }
}
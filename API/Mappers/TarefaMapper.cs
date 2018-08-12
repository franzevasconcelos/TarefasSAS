using System.Collections.Generic;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Mappers {
    public class TarefaMapper : Profile {
        public TarefaMapper() {
            CreateMap<Interface.Tarefa, Tarefa>()
                .ConstructUsing(ObterTarefa)
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<Tarefa, Interface.Tarefa>()
                .ForMember(dest => dest.IdProfessor, opt => opt.MapFrom(src => src.Professor.Id));
        }

        private Tarefa ObterTarefa(Interface.Tarefa tarefaInterface) {
            var questoes = new Questoes(NhibernateSetup.GetSession());
            var professores = new Professores(NhibernateSetup.GetSession());

            var listaQuestoes = new List<Questao>();
            foreach (var questao in tarefaInterface.Questoes) {
                listaQuestoes.Add(questoes.Por(questao.Id));
            }

            var professor = professores.Por(tarefaInterface.IdProfessor);

            var tarefa = new Tarefa {
                                        Questoes = listaQuestoes,
                                        Professor = professor
                                    };

            return tarefa;
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Mappers {
    public class ResolucaoMapper : Profile {
        public ResolucaoMapper() {
            CreateMap<ResolucaoTarefa, Interface.Resolucao>()
                .ConstructUsing(ObterResolucao)
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<Tarefa, Interface.Resolucao>()
                .ForMember(dest => dest.Questoes, opt => opt.MapFrom(src => src.Questoes))
                .ForMember(dest => dest.IdTarefa, opt => opt.MapFrom(src => src.Id));

            CreateMap<Interface.Resolucao, List<ResolucaoQuestao>>()
                .ConstructUsing(ObterResolucaoQuestao)
                .ForAllMembers(opt => opt.Ignore());
        }

        private List<ResolucaoQuestao> ObterResolucaoQuestao(Interface.Resolucao arg) {
            var resolucoes = new Resolucoes(NhibernateSetup.GetSession());
            var listaResolucoes = resolucoes.ResolucaoQuestaoPorTarefaEAluno(arg.IdTarefa, arg.Questoes.First().IdAluno).ToList();

            if (listaResolucoes.Any()) {
                foreach (var resolucaoQuestao in listaResolucoes) {
                    resolucaoQuestao.Comentario = arg.Questoes.First(q => q.Id == resolucaoQuestao.Questao.Id).Cometario;
                    resolucaoQuestao.Resposta = arg.Questoes.First(q => q.Id == resolucaoQuestao.Questao.Id).Resposta;
                }
            }
            
            var tarefas = new Tarefas(NhibernateSetup.GetSession());
            var alunos = new Alunos(NhibernateSetup.GetSession());
            var questoes = new Questoes(NhibernateSetup.GetSession());

            var tarefa = tarefas.Por(arg.IdTarefa);
            var aluno = alunos.Por(arg.Questoes.First().IdAluno);

            var novasQuestoes = new List<Interface.Questao>();
            foreach (var questao in arg.Questoes.Where(q => q.Resposta != null)) {
                if (listaResolucoes.All(l => l.Questao.Id != questao.Id)) {
                    novasQuestoes.Add(questao);
                }
            }

            foreach (var questao in novasQuestoes) {
                var resolucaoQuestao = new ResolucaoQuestao();
                resolucaoQuestao.Tarefa = tarefa;
                resolucaoQuestao.Aluno = aluno;
                resolucaoQuestao.Resposta = questao.Resposta;
                resolucaoQuestao.Comentario = questao.Cometario;

                resolucaoQuestao.Questao = questoes.Por(questao.Id);

                listaResolucoes.Add(resolucaoQuestao);
            }

            return listaResolucoes;
        }

        private Interface.Resolucao ObterResolucao(ResolucaoTarefa arg) {
            var resolucao = new Interface.Resolucao {
                                                        Id = arg.Id,
                                                        IdTarefa = arg.Tarefa.Id,
                                                        Enviada = arg.Enviada,
                                                        Nota = arg.Nota
                                                    };
            return resolucao;
        }
    }
}
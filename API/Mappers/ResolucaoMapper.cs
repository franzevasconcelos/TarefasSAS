using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;
using Tarefa = TarefasSAS.API.Entidades.Tarefa;

namespace TarefasSAS.API.Mappers {
    public class ResolucaoMapper : Profile {
        public ResolucaoMapper() {
            CreateMap<ResolucaoTarefa, Interface.Resolucao>()
                .ConstructUsing(ObterResolucaoAPartirResolucaoTarefa)
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<Tarefa, Interface.Resolucao>()
                .ForMember(dest => dest.Questoes, opt => opt.MapFrom(src => src.Questoes))
                .ForMember(dest => dest.IdTarefa, opt => opt.MapFrom(src => src.Id));

            CreateMap<Interface.Resolucao, List<ResolucaoQuestao>>()
                .ConstructUsing(ObterResolucaoQuestao)
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<List<ResolucaoQuestao>, Interface.Resolucao>()
                .ConstructUsing(Ctor)
                .ForAllMembers(opt => opt.Ignore());
        }

        private Interface.Resolucao Ctor(List<ResolucaoQuestao> arg) {
            var resolucao = new Interface.Resolucao();

            resolucao.Questoes = Mapper.Map<List<Interface.Questao>>(arg);

            var resolucoes = new Resolucoes(NhibernateSetup.GetSession());
            var resolucaoTarefa = resolucoes.ResolucaoTarefaPorTarefaEAluno(arg[0].Aluno.Id, arg[0].Tarefa.Id);

            resolucao.Enviada = resolucaoTarefa.Enviada;
            resolucao.Nota = resolucaoTarefa.Nota;

            return resolucao;
        }

        private List<ResolucaoQuestao> ObterResolucaoQuestao(Interface.Resolucao arg) {
            var resolucoes = new Resolucoes(NhibernateSetup.GetSession());
            var listaResolucoes = resolucoes.ResolucaoQuestaoPorTarefaEAluno(arg.IdTarefa, arg.Questoes.First().IdAluno).ToList();

            if (listaResolucoes.Any()) {
                foreach (var resolucaoQuestao in listaResolucoes) {
                    resolucaoQuestao.Comentario = arg.Questoes.First(q => q.Id == resolucaoQuestao.Questao.Id).Comentario;
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
                resolucaoQuestao.Comentario = questao.Comentario;

                resolucaoQuestao.Questao = questoes.Por(questao.Id);

                listaResolucoes.Add(resolucaoQuestao);
            }

            return listaResolucoes;
        }

        private Interface.Resolucao ObterResolucaoAPartirResolucaoTarefa(ResolucaoTarefa arg) {
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
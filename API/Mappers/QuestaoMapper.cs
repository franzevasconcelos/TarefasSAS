﻿using AutoMapper;
using TarefasSAS.API.Configuracoes;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace TarefasSAS.API.Mappers {
    public class QuestaoMapper : Profile {
        public QuestaoMapper() {
            CreateMap<Interface.Questao, Questao>()
                .ForMember(dest => dest.Professor, opt => opt.MapFrom(src => ObterProfessor(src.IdProfessor)));

            CreateMap<Questao, Interface.Questao>()
                .ForMember(dest => dest.IdProfessor, opt => opt.MapFrom(src => src.Professor.Id));

            CreateMap<ResolucaoQuestao, Interface.Questao>()
                .ConstructUsing(Ctor)
                .ForAllMembers(opt => opt.Ignore());
        }

        private Interface.Questao Ctor(ResolucaoQuestao arg) {
            var questao = Mapper.Map<Interface.Questao>(arg.Questao);
            questao.Resposta = arg.Resposta;
            questao.Comentario = arg.Comentario;
            
            return questao;
        }

        private Professor ObterProfessor(int id) {
            var professores= new Professores(NhibernateSetup.GetSession());
            return professores.Por(id);
        }
    }
}
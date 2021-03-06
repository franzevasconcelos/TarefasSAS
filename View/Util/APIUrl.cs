﻿using System.Configuration;

namespace View.Util {
    public static class APIUrl {
        public static string EnderecoBase() {
            return (string) new AppSettingsReader().GetValue("Endereco.API", typeof(string));
        }

        public static string Login(string login) {
            return EnderecoBase() + $"usuario?login={login}";
        }

        public static string SalvarQuestao() {
            return EnderecoBase() + "questoes/salvar";
        }

        public static string QuestoesPorProfessor(int idProfessor) {
            return EnderecoBase() + "questoes/PorProfessor?idProfessor=" + idProfessor;
        }

        public static string SalvarTarefa() {
            return EnderecoBase() + "tarefas/salvar";
        }

        public static string Tarefas(int idProfessor) {
            return EnderecoBase() + $"tarefas/listar?idProfessor={idProfessor}";
        }

        public static string TarefasAResolver(int idAluno) {
            return EnderecoBase() + $"tarefas/aresolver?idAluno={idAluno}";
        }

        public static string TarefaPorId(int idTarefa, int idAluno) {
            return EnderecoBase() + $"tarefas/porid?idTarefa={idTarefa}&idAluno={idAluno}";
        }

        public static string SalvarRespostasDeTarefa() {
            return EnderecoBase() + "resolucaoquestao/salvar";
        }

        public static string ResolucaoQuestaoObterResolucaoAluno(int idAluno, int idTarefa) {
            return EnderecoBase() + $"resolucaoquestao/ObterResolucaoAluno?idAluno={idAluno}&idTarefa={idTarefa}";
        }

        public static string Turmas(int idProfessor) {
            return EnderecoBase() + $"turma/listar?idProfessor={idProfessor}";
        }

        public static string EnviarTarefaTurma() {
            return EnderecoBase() + $"turma/SalvarTarefa";
        }

        public static string TurmaObterAlunos(int idTurma, int idTarefa) {
            return EnderecoBase() + $"turma/obteralunos?idTurma={idTurma}&idTarefa={idTarefa}";
        }

        public static string EnviarTarefaParaCorrecao() {
            return EnderecoBase() + $"resolucaotarefa/LiberarParaCorrecao";
        }

        public static string ResolucaoQuestaoSalvarResolucaoAluno() {
            return EnderecoBase() + $"resolucaoquestao/SalvarResolucaoAluno";
        }
    }
}
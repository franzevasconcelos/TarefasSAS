using System.Collections.Generic;
using NUnit.Framework;
using TarefasSAS.API.Entidades;
using TarefasSAS.API.Persistencia;

namespace API.Tests.Persistencia {
    [TestFixture]
    class TestTarefas : PersistenciaBaseTest {
        protected override string NomeXmlDataset => "tarefas.xml";

        [Test]
        public void DeveTrazerTodasAsTarefasDeUmProfessorCorretamente() {
            var tarefas = new Tarefas(Sessao);

            var tarefasEncontradas = tarefas.PorProfessor(1);

            Assert.That(tarefasEncontradas.Count, Is.EqualTo(3));
            Assert.That(tarefasEncontradas[0].Id, Is.EqualTo(1));
            Assert.That(tarefasEncontradas[1].Id, Is.EqualTo(2));
            Assert.That(tarefasEncontradas[2].Id, Is.EqualTo(3));
        }

        [Test]
        public void DeveTrazerTodasAsQuestoesDeUmaTarefa() {
            var tarefas = new Tarefas(Sessao);

            var questoresEncontradas = tarefas.QuestoesPorTarefa(3);

            Assert.That(questoresEncontradas.Count, Is.EqualTo(2));
            Assert.That(questoresEncontradas[0].Id, Is.EqualTo(3));
            Assert.That(questoresEncontradas[1].Id, Is.EqualTo(4));
        }

        [Test]
        public void DeveSalvarTarefaCorretamente()
        {
            var tarefas = new Tarefas(Sessao);

            var professor = Sessao.Get<Professor>(1);

            var novaTarefa = new Tarefa
            {
                Professor = professor,
                Questoes = new List<Questao>()
            };

            var q3 = Sessao.Get<Questao>(3);
            var q4 = Sessao.Get<Questao>(4);

            novaTarefa.Questoes.Add(q3);
            novaTarefa.Questoes.Add(q4);

            tarefas.Salvar(novaTarefa);

            var tarefa = Sessao.Get<Tarefa>(4);

            Assert.That(tarefa, Is.Not.Null);
        }

        [Test]
        public void DeveSalvarQuestoesEmTarefaCorretamente()
        {
            var tarefas = new Tarefas(Sessao);

            var professor = Sessao.Get<Professor>(1);

            var novaTarefa = new Tarefa
            {
                Professor = professor,
                Questoes = new List<Questao>()
            };

            var q3 = Sessao.Get<Questao>(3);
            var q4 = Sessao.Get<Questao>(4);

            novaTarefa.Questoes.Add(q3);
            novaTarefa.Questoes.Add(q4);

            tarefas.Salvar(novaTarefa);

            var tarefa = Sessao.Get<Tarefa>(4);

            Assert.That(tarefa.Questoes.Count, Is.EqualTo(2));
            Assert.That(tarefa.Questoes[0].Id, Is.EqualTo(3));
            Assert.That(tarefa.Questoes[1].Id, Is.EqualTo(4));
        }

        [Test]
        public void DeveSalvarProfessorEmTarefaCorretamente()
        {
            var tarefas = new Tarefas(Sessao);

            var professor = Sessao.Get<Professor>(1);

            var novaTarefa = new Tarefa
            {
                Professor = professor,
                Questoes = new List<Questao>()
            };

            var q3 = Sessao.Get<Questao>(3);
            var q4 = Sessao.Get<Questao>(4);

            novaTarefa.Questoes.Add(q3);
            novaTarefa.Questoes.Add(q4);

            tarefas.Salvar(novaTarefa);

            var tarefa = Sessao.Get<Tarefa>(4);

            Assert.That(tarefa.Professor, Is.Not.Null);
            Assert.That(tarefa.Professor.Id, Is.EqualTo(1));
        }
    }
}
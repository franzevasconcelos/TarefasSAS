﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Interface;
using Newtonsoft.Json;
using View.Util;
using View.ViewModels;

namespace View.Controllers {
    [Authorize(Roles = "Professor")]
    public class ProfessorController : Controller {
        [HttpGet]
        public ActionResult NovaTarefa() {
            using (var client = new WebClient()) {
                try {
                    var obj = client.DownloadString(APIUrl.QuestoesPorProfessor(Convert.ToInt32(User.Identity.Name)));
                    var questoes = JsonConvert.DeserializeObject(obj, typeof(List<Interface.Questao>));

                    var viewModel = Mapper.Map<TarefaViewModel>(questoes);
                    return View(viewModel);
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult NovaTarefa(TarefaViewModel viewModel) {
            var tarefa = Mapper.Map<Interface.Tarefa>(viewModel);
            tarefa.IdProfessor = Convert.ToInt32(User.Identity.Name);

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try {
                    client.UploadString(APIUrl.SalvarTarefa(), "POST", JsonConvert.SerializeObject(tarefa));
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult NovaQuestao() {
            return View();
        }

        [HttpPost]
        public ActionResult NovaQuestao(QuestaoViewModel viewModel) {
            var questao = Mapper.Map<Interface.Questao>(viewModel);
            questao.IdProfessor = Convert.ToInt32(User.Identity.Name);

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try {
                    client.UploadString(APIUrl.SalvarQuestao(), "POST", JsonConvert.SerializeObject(questao));
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return RedirectToAction("NovaQuestao");
        }

        [HttpGet]
        public ActionResult Index() {
            using (var client = new WebClient()) {
                try {
                    var obj = client.DownloadString(APIUrl.Tarefas(Convert.ToInt32(User.Identity.Name)));
                    var tarefas = JsonConvert.DeserializeObject(obj, typeof(List<Interface.Tarefa>));

                    var viewModel = Mapper.Map<List<TarefaViewModel>>(tarefas);
                    return View(viewModel);
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult EnviarTarefaATurma(int idTarefa) {
            using (var client = new WebClient()) {
                try {
                    var obj = client.DownloadString(APIUrl.Turmas(Convert.ToInt32(User.Identity.Name)));
                    var turmas = JsonConvert.DeserializeObject(obj, typeof(List<Interface.Turma>));

                    var viewModel = Mapper.Map<TarefaViewModel>(turmas);
                    viewModel.Id = idTarefa;

                    return View(viewModel);
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult EnviarTarefaATurma(TarefaViewModel viewModel) {
            var tarefaMapeada = Mapper.Map<TarefaTurma>(viewModel);

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try {
                    client.UploadString(APIUrl.EnviarTarefaTurma(), "POST", JsonConvert.SerializeObject(tarefaMapeada));
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CorrigirTarefa(int idTarefa) {
            using (var client = new WebClient()) {
                try {
                    var obj = client.DownloadString(APIUrl.Turmas(Convert.ToInt32(User.Identity.Name)));
                    var turmas = JsonConvert.DeserializeObject(obj, typeof(List<Interface.Turma>));

                    var viewModel = Mapper.Map<TarefaViewModel>(turmas);
                    viewModel.Id = idTarefa;

                    return View(viewModel);
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View();
                }
            }
        }

        public ActionResult ObterAlunos(TarefaViewModel viewModel) {
            using (var client = new WebClient()) {
                try {
                    var obj = client.DownloadString(APIUrl.TurmaObterAlunos(viewModel.TurmaEscolhida, viewModel.Id));
                    var alunos = JsonConvert.DeserializeObject(obj, typeof(List<Interface.AlunoTarefa>));

                    var listaAlunoViewModel = Mapper.Map<List<AlunoTarefaViewModel>>(alunos);

                    foreach (var lista in listaAlunoViewModel) {
                        lista.IdTarefa = viewModel.Id;
                    }

                    return PartialView("_ListaAlunos", listaAlunoViewModel);
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return null;
                }
            }
        }

        public ActionResult AbrirResolucao(int idAluno, int idTarefa) {
            using (var client = new WebClient()) {
                try {
                    var obj = client.DownloadString(APIUrl.ResolucaoQuestaoObterResolucaoAluno(idAluno, idTarefa));
                    var resolucoes = JsonConvert.DeserializeObject(obj, typeof(Interface.Resolucao));

                    var viewModel = Mapper.Map<ResolucaoViewModel>(resolucoes);
                    viewModel.IdTarefa = idTarefa;

                    foreach (var questao in viewModel.Questoes) {
                        questao.IdAluno = idAluno;
                    }

                    return View(viewModel);
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Abrirresolucao(ResolucaoViewModel viewModel) {
            var resolucao = Mapper.Map<Resolucao>(viewModel);
            resolucao.IdAluno = viewModel.Questoes[0].IdAluno;

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try {
                    client.UploadString(APIUrl.ResolucaoQuestaoSalvarResolucaoAluno(), "PUT",
                                        JsonConvert.SerializeObject(resolucao));

                    return RedirectToAction("Index");
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
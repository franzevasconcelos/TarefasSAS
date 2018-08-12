using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Interface;
using Newtonsoft.Json;
using View.Util;
using View.ViewModels;

namespace View.Controllers {
    [Authorize(Roles = "Aluno")]
    public class AlunoController : Controller {
        [HttpGet]
        public ActionResult Index() {
            using (var client = new WebClient()) {
                try {
                    var obj = client.DownloadString(APIUrl.TarefasAResolver(Convert.ToInt32(User.Identity.Name)));
                    var resolucoes = JsonConvert.DeserializeObject(obj, typeof(List<Interface.Resolucao>));

                    var viewModel = Mapper.Map<List<ResolucaoViewModel>>(resolucoes);

                    return View(viewModel);
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View();
                }
            }
        }

        public ActionResult ResolverTarefa(int idTarefa) {
            using (var client = new WebClient()) {
                try {
                    var obj = client.DownloadString(APIUrl.TarefaPorId(idTarefa, Convert.ToInt32(User.Identity.Name)));
                    var resolucoes = JsonConvert.DeserializeObject(obj, typeof(Interface.Resolucao));

                    var viewModel = Mapper.Map<ResolucaoViewModel>(resolucoes);

                    return View(viewModel);
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult ResolverTarefa(ResolucaoViewModel viewModel) {
            var resolucao = Mapper.Map<Resolucao>(viewModel);

            foreach (var questao in resolucao.Questoes) {
                questao.IdAluno = Convert.ToInt32(User.Identity.Name);
            }

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try {
                    client.UploadString(APIUrl.SalvarRespostasDeTarefa(), "POST",
                                        JsonConvert.SerializeObject(resolucao));

                    return RedirectToAction("Index");
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction("ResolverTarefa", new {IdTarefa = viewModel.IdTarefa});
                }
            }
        }

        public ActionResult EnviarParaCorrecao(int idResolucao) {
            var resolucao = new Resolucao {
                                              Id = idResolucao,
                                              IdAluno = Convert.ToInt32(User.Identity.Name)
                                          };

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try {
                    client.UploadString(APIUrl.EnviarTarefaParaCorrecao(), "PUT",
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
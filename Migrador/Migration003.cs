using System.Collections.Generic;
using System.IO;
using System.Net;
using FluentMigrator;
using Newtonsoft.Json.Linq;

namespace Migrador {
    [Migration(3, "Novas colunas em aluno e professor. Carregamento de alguns dados no banco.")]
    public class Migration003 : Migration {
        public override void Up() {
            Create.Table("Usuario")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey()
                  .Identity()
                  .WithColumn("login")
                  .AsString(100);

            Alter.Table("Professor")
                 .AddColumn("usuario_id")
                 .AsInt32()
                 .ForeignKey("usuario", "id")
                 .Nullable();

            Alter.Table("Aluno")
                 .AddColumn("email")
                 .AsString(50)
                 .Nullable()
                 .AddColumn("nascimento")
                 .AsString(20)
                 .Nullable()
                 .AddColumn("usuario_id")
                 .AsInt32()
                 .ForeignKey("usuario", "id")
                 .Nullable();

            PreencherTabelas();
        }

        public void PreencherTabelas() {
            var alunos = ObterAlunos();

            Insert.IntoTable("Usuario")
                  .Row(new {login = "professor1"});

            Insert.IntoTable("Professor")
                  .Row(new {
                               nome = "Professor 1",
                               usuario_id = 1
                           });

            Insert.IntoTable("Turma")
                  .Row(new {
                               nome = "Turma 1",
                               professor_id = 1
                           });

            Insert.IntoTable("Turma")
                  .Row(new {
                               nome = "Turma 2",
                               professor_id = 1
                           });

            Insert.IntoTable("Usuario")
                  .Row(new {login = "professor2"});

            Insert.IntoTable("Professor")
                  .Row(new {
                               nome = "Professor 2",
                               usuario_id = 2
                           });

            Insert.IntoTable("Turma")
                  .Row(new {
                               nome = "Turma 3",
                               professor_id = 2
                           });

            int usuarioId = 3;
            int turma = 1;
            int totalTurma = 0;
            foreach (var aluno in alunos) {
                if (totalTurma >= 3) {
                    turma++;
                    totalTurma = 0;
                }

                Insert.IntoTable("Usuario")
                      .Row(new {login = aluno.Login});

                Insert.IntoTable("Aluno")
                      .Row(new {
                                   nome = aluno.Nome,
                                   email = aluno.Email,
                                   nascimento = aluno.Nascimento,
                                   usuario_id = usuarioId++,
                                   turma_id = turma
                               });

                totalTurma++;
            }
        }

        private List<Aluno> ObterAlunos() {
            var webClient = new WebClient();

            var stream = webClient.OpenRead("https://randomuser.me/api/?nat=br&results=9");

            var streamReader = new StreamReader(stream);
            var json = streamReader.ReadToEnd();
            var obj = (dynamic) JObject.Parse(json);

            var alunos = new List<Aluno>();
            for (var i = 0; i < 9; i++) {
                alunos.Add(new Aluno {
                                         Nome = obj.results[i].name.first + " " + obj.results[i].name.last,
                                         Email = obj.results[i].email,
                                         Nascimento = obj.results[i].dob.date,
                                         Login = "aluno" + (i + 1)
                                     });
            }

            return alunos;
        }

        public override void Down() { }

        private class Aluno {
            internal string Nome { get; set; }
            internal string Email { get; set; }
            internal string Nascimento { get; set; }
            internal string Login { get; set; }
        }
    }
}
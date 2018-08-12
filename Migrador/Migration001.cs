using FluentMigrator;

namespace Migrador {
    [Migration(1, "Criação do banco inicial")]
    public class Migration001 : Migration {
        public override void Up() {
            Create.Table("Professor")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_professor")
                  .Identity()
                  .WithColumn("nome")
                  .AsString(100)
                  .WithColumn("usuario_id")
                  .AsInt32()
                  .ForeignKey("usuario", "id");

            Create.Table("Turma")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_turma")
                  .Identity()
                  .WithColumn("nome")
                  .AsString(100)
                  .WithColumn("professor_id")
                  .AsInt32()
                  .ForeignKey("professor", "id");

            Create.Table("Aluno")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_aluno")
                  .Identity()
                  .WithColumn("nome")
                  .AsString(100)
                  .WithColumn("turma_id")
                  .AsInt32()
                  .ForeignKey("turma", "id")
                  .Nullable()
                  .WithColumn("email")
                  .AsString(50)
                  .Nullable()
                  .WithColumn("nascimento")
                  .AsString(20)
                  .Nullable()
                  .WithColumn("usuario_id")
                  .AsInt32()
                  .ForeignKey("usuario", "id");

            Create.Table("Tarefa")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_tarefa")
                  .Identity()
                  .WithColumn("professor_id")
                  .AsInt32()
                  .ForeignKey("professor", "id");

            Create.Table("Questao")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_questao")
                  .Identity()
                  .WithColumn("pergunta")
                  .AsString()
                  .WithColumn("professor_id")
                  .AsInt32()
                  .ForeignKey("professor", "id");

            Create.Table("TarefaQuestao")
                  .WithColumn("tarefa_id")
                  .AsInt32()
                  .ForeignKey("tarefa", "id")
                  .WithColumn("questao_id")
                  .AsInt32()
                  .ForeignKey("questao", "id");

            Create.Table("Usuario")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey()
                  .Identity()
                  .WithColumn("login")
                  .AsString(100);
        }

        public override void Down() { }
    }
}
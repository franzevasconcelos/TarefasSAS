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
                  .AsString(100);

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
                  .Nullable();
            
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
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_questao_tarefa")
                  .Identity()
                  .WithColumn("tarefa_id")
                  .AsInt32()
                  .ForeignKey("tarefa", "id")
                  .WithColumn("questao_id")
                  .AsInt32()
                  .ForeignKey("questao", "id");
        }

        public override void Down() { }
    }
}
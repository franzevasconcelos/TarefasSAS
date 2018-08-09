using FluentMigrator;

namespace Migrador {
    [Migration(2, "Adição de resolução e relação turma com tarefa")]
    public class Migration002 : Migration {
        public override void Up() {
            Create.Table("Resolucao")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_resolucao")
                  .Identity()
                  .WithColumn("comentario")
                  .AsString(100)
                  .WithColumn("resposta")
                  .AsString(100)
                  .WithColumn("nota")
                  .AsDouble()
                  .WithColumn("enviada")
                  .AsBoolean()
                  .WithColumn("tarefa_id")
                  .AsInt32()
                  .ForeignKey("tarefa", "id")
                  .WithColumn("questao_id")
                  .AsInt32()
                  .ForeignKey("questao", "id")
                  .WithColumn("aluno_id")
                  .AsInt32()
                  .ForeignKey("aluno", "id");

            Create.Table("TarefaTurma")
                  .WithColumn("tarefa_id")
                  .AsInt32()
                  .ForeignKey("tarefa", "id")
                  .WithColumn("turma_id")
                  .AsInt32()
                  .ForeignKey("turma", "id");
        }

        public override void Down() { }
    }
}
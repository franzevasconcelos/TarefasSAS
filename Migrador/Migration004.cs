using FluentMigrator;

namespace Migrador {
    [Migration(4, "Carregamento de alguns dados no banco.")]
    public class Migration004 : Migration {
        public override void Up() {
            Rename.Table("Resolucao").InSchema("dbo").To("ResolucaoQuestao");

            Create.Table("ResolucaoTarefa")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_resolucaotarefa")
                  .Identity()
                  .WithColumn("nota")
                  .AsDouble()
                  .Nullable()
                  .WithColumn("enviada")
                  .AsBoolean()
                  .Nullable()
                  .WithColumn("tarefa_id")
                  .AsInt32()
                  .ForeignKey("Tarefa", "id")
                  .WithColumn("aluno_id")
                  .AsInt32()
                  .ForeignKey("Aluno", "id");
        }


        public override void Down() { }
    }
}
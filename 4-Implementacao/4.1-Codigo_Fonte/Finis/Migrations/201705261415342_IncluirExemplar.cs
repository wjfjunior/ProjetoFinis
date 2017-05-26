namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncluirExemplar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exemplares", "editora_id", "dbo.Fornecedores");
            DropForeignKey("dbo.Exemplares", "idioma_id", "dbo.Idiomas");
            DropForeignKey("dbo.Exemplares", "sessao_id", "dbo.Sessoes");
            DropForeignKey("dbo.Idiomas", "pais_id", "dbo.Paises");
            DropIndex("dbo.Exemplares", new[] { "editora_id" });
            DropIndex("dbo.Exemplares", new[] { "idioma_id" });
            DropIndex("dbo.Exemplares", new[] { "sessao_id" });
            DropIndex("dbo.Idiomas", new[] { "pais_id" });
            RenameColumn(table: "dbo.Exemplares", name: "editora_id", newName: "editoraId");
            RenameColumn(table: "dbo.Exemplares", name: "idioma_id", newName: "idiomaId");
            RenameColumn(table: "dbo.Exemplares", name: "sessao_id", newName: "sessaoId");
            RenameColumn(table: "dbo.Idiomas", name: "pais_id", newName: "paisId");
            AddColumn("dbo.Exemplares", "isbn", c => c.Int(nullable: false));
            AddColumn("dbo.Exemplares", "edicao", c => c.Int(nullable: false));
            AddColumn("dbo.Exemplares", "preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Exemplares", "peso", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Exemplares", "quantidade", c => c.Int(nullable: false));
            AddColumn("dbo.Sessoes", "subsessaoId", c => c.Int());
            AlterColumn("dbo.Exemplares", "editoraId", c => c.Int(nullable: false));
            AlterColumn("dbo.Exemplares", "idiomaId", c => c.Int(nullable: false));
            AlterColumn("dbo.Exemplares", "sessaoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Idiomas", "paisId", c => c.Int(nullable: false));
            CreateIndex("dbo.Exemplares", "editoraId");
            CreateIndex("dbo.Exemplares", "idiomaId");
            CreateIndex("dbo.Exemplares", "sessaoId");
            CreateIndex("dbo.Idiomas", "paisId");
            CreateIndex("dbo.Sessoes", "subsessaoId");
            AddForeignKey("dbo.Sessoes", "subsessaoId", "dbo.Sessoes", "id");
            AddForeignKey("dbo.Exemplares", "editoraId", "dbo.Fornecedores", "id", cascadeDelete: true);
            AddForeignKey("dbo.Exemplares", "idiomaId", "dbo.Idiomas", "id", cascadeDelete: true);
            AddForeignKey("dbo.Exemplares", "sessaoId", "dbo.Sessoes", "id", cascadeDelete: true);
            AddForeignKey("dbo.Idiomas", "paisId", "dbo.Paises", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Idiomas", "paisId", "dbo.Paises");
            DropForeignKey("dbo.Exemplares", "sessaoId", "dbo.Sessoes");
            DropForeignKey("dbo.Exemplares", "idiomaId", "dbo.Idiomas");
            DropForeignKey("dbo.Exemplares", "editoraId", "dbo.Fornecedores");
            DropForeignKey("dbo.Sessoes", "subsessaoId", "dbo.Sessoes");
            DropIndex("dbo.Sessoes", new[] { "subsessaoId" });
            DropIndex("dbo.Idiomas", new[] { "paisId" });
            DropIndex("dbo.Exemplares", new[] { "sessaoId" });
            DropIndex("dbo.Exemplares", new[] { "idiomaId" });
            DropIndex("dbo.Exemplares", new[] { "editoraId" });
            AlterColumn("dbo.Idiomas", "paisId", c => c.Int());
            AlterColumn("dbo.Exemplares", "sessaoId", c => c.Int());
            AlterColumn("dbo.Exemplares", "idiomaId", c => c.Int());
            AlterColumn("dbo.Exemplares", "editoraId", c => c.Int());
            DropColumn("dbo.Sessoes", "subsessaoId");
            DropColumn("dbo.Exemplares", "quantidade");
            DropColumn("dbo.Exemplares", "peso");
            DropColumn("dbo.Exemplares", "preco");
            DropColumn("dbo.Exemplares", "edicao");
            DropColumn("dbo.Exemplares", "isbn");
            RenameColumn(table: "dbo.Idiomas", name: "paisId", newName: "pais_id");
            RenameColumn(table: "dbo.Exemplares", name: "sessaoId", newName: "sessao_id");
            RenameColumn(table: "dbo.Exemplares", name: "idiomaId", newName: "idioma_id");
            RenameColumn(table: "dbo.Exemplares", name: "editoraId", newName: "editora_id");
            CreateIndex("dbo.Idiomas", "pais_id");
            CreateIndex("dbo.Exemplares", "sessao_id");
            CreateIndex("dbo.Exemplares", "idioma_id");
            CreateIndex("dbo.Exemplares", "editora_id");
            AddForeignKey("dbo.Idiomas", "pais_id", "dbo.Paises", "id");
            AddForeignKey("dbo.Exemplares", "sessao_id", "dbo.Sessoes", "id");
            AddForeignKey("dbo.Exemplares", "idioma_id", "dbo.Idiomas", "id");
            AddForeignKey("dbo.Exemplares", "editora_id", "dbo.Fornecedores", "id");
        }
    }
}

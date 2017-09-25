namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriarProduto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 50),
                        descricao = c.String(nullable: false, maxLength: 200),
                        precoCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        precoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quantidade = c.Int(nullable: false),
                        unidadeMedidaId = c.Int(nullable: false),
                        fabricante = c.String(nullable: false, maxLength: 50),
                        estoqueMinimo = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UnidadesMedida", t => t.unidadeMedidaId, cascadeDelete: true)
                .Index(t => t.unidadeMedidaId);
            
            CreateTable(
                "dbo.UnidadesMedida",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        grandeza = c.String(nullable: false, maxLength: 50),
                        unidade = c.String(nullable: false, maxLength: 20),
                        simbolo = c.String(nullable: false, maxLength: 3),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtos", "unidadeMedidaId", "dbo.UnidadesMedida");
            DropIndex("dbo.Produtos", new[] { "unidadeMedidaId" });
            DropTable("dbo.UnidadesMedida");
            DropTable("dbo.Produtos");
        }
    }
}

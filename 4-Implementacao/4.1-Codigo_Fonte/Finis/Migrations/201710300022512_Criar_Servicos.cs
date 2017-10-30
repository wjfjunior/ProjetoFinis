namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criar_Servicos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        clienteId = c.Int(nullable: false),
                        descricao = c.String(maxLength: 200),
                        dataPedido = c.DateTime(nullable: false),
                        situacao = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clientes", t => t.clienteId, cascadeDelete: true)
                .Index(t => t.clienteId);
            
            CreateTable(
                "dbo.Servicos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 50),
                        descricao = c.String(maxLength: 200),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quantidade = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Exemplares", "Pedido_id", c => c.Int());
            CreateIndex("dbo.Exemplares", "Pedido_id");
            AddForeignKey("dbo.Exemplares", "Pedido_id", "dbo.Pedidos", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exemplares", "Pedido_id", "dbo.Pedidos");
            DropForeignKey("dbo.Pedidos", "clienteId", "dbo.Clientes");
            DropIndex("dbo.Pedidos", new[] { "clienteId" });
            DropIndex("dbo.Exemplares", new[] { "Pedido_id" });
            DropColumn("dbo.Exemplares", "Pedido_id");
            DropTable("dbo.Servicos");
            DropTable("dbo.Pedidos");
        }
    }
}

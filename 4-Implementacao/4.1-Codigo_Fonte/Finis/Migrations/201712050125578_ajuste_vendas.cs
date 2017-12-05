namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajuste_vendas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendas", "formaPagamento", c => c.Int(nullable: false));
            AddColumn("dbo.Vendas", "clienteId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vendas", "clienteId");
            AddForeignKey("dbo.Vendas", "clienteId", "dbo.Clientes", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendas", "clienteId", "dbo.Clientes");
            DropIndex("dbo.Vendas", new[] { "clienteId" });
            DropColumn("dbo.Vendas", "clienteId");
            DropColumn("dbo.Vendas", "formaPagamento");
        }
    }
}

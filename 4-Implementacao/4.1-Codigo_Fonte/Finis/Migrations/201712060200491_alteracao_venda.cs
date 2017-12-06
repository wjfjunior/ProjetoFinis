namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteracao_venda : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Itens", "VendaId", "dbo.Vendas");
            DropIndex("dbo.Itens", new[] { "VendaId" });
            CreateTable(
                "dbo.ItemVendas",
                c => new
                    {
                        Item_id = c.Int(nullable: false),
                        Venda_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Item_id, t.Venda_id })
                .ForeignKey("dbo.Itens", t => t.Item_id, cascadeDelete: false)
                .ForeignKey("dbo.Vendas", t => t.Venda_id, cascadeDelete: false)
                .Index(t => t.Item_id)
                .Index(t => t.Venda_id);
            
            DropColumn("dbo.Itens", "VendaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Itens", "VendaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ItemVendas", "Venda_id", "dbo.Vendas");
            DropForeignKey("dbo.ItemVendas", "Item_id", "dbo.Itens");
            DropIndex("dbo.ItemVendas", new[] { "Venda_id" });
            DropIndex("dbo.ItemVendas", new[] { "Item_id" });
            DropTable("dbo.ItemVendas");
            CreateIndex("dbo.Itens", "VendaId");
            AddForeignKey("dbo.Itens", "VendaId", "dbo.Vendas", "id", cascadeDelete: true);
        }
    }
}

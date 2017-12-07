namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificacaovendas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemVendas", "Item_id", "dbo.Itens");
            DropForeignKey("dbo.ItemVendas", "Venda_id", "dbo.Vendas");
            DropIndex("dbo.ItemVendas", new[] { "Item_id" });
            DropIndex("dbo.ItemVendas", new[] { "Venda_id" });
            CreateTable(
                "dbo.ItensVenda",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        indice = c.Int(nullable: false),
                        quantidade = c.Int(nullable: false),
                        precoTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        vendaId = c.Int(nullable: false),
                        itemId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Itens", t => t.itemId, cascadeDelete: false)
                .ForeignKey("dbo.Vendas", t => t.vendaId, cascadeDelete: false)
                .Index(t => t.vendaId)
                .Index(t => t.itemId);
            
            AddColumn("dbo.Itens", "quantidadeEstoque", c => c.Int(nullable: false));
            DropColumn("dbo.Itens", "precoTotal");
            DropColumn("dbo.Itens", "quantidade");
            DropTable("dbo.ItemVendas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ItemVendas",
                c => new
                    {
                        Item_id = c.Int(nullable: false),
                        Venda_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Item_id, t.Venda_id });
            
            AddColumn("dbo.Itens", "quantidade", c => c.Int(nullable: false));
            AddColumn("dbo.Itens", "precoTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.ItensVenda", "vendaId", "dbo.Vendas");
            DropForeignKey("dbo.ItensVenda", "itemId", "dbo.Itens");
            DropIndex("dbo.ItensVenda", new[] { "itemId" });
            DropIndex("dbo.ItensVenda", new[] { "vendaId" });
            DropColumn("dbo.Itens", "quantidadeEstoque");
            DropTable("dbo.ItensVenda");
            CreateIndex("dbo.ItemVendas", "Venda_id");
            CreateIndex("dbo.ItemVendas", "Item_id");
            AddForeignKey("dbo.ItemVendas", "Venda_id", "dbo.Vendas", "id", cascadeDelete: true);
            AddForeignKey("dbo.ItemVendas", "Item_id", "dbo.Itens", "id", cascadeDelete: true);
        }
    }
}

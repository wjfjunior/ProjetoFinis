namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriarMarca : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 50),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Produtos", "marcaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Produtos", "marcaId");
            AddForeignKey("dbo.Produtos", "marcaId", "dbo.Marcas", "id", cascadeDelete: true);
            DropColumn("dbo.Produtos", "fabricante");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produtos", "fabricante", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Produtos", "marcaId", "dbo.Marcas");
            DropIndex("dbo.Produtos", new[] { "marcaId" });
            DropColumn("dbo.Produtos", "marcaId");
            DropTable("dbo.Marcas");
        }
    }
}

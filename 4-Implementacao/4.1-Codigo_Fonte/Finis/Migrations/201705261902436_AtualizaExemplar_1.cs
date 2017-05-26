namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizaExemplar_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exemplares", "precoCompra", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Exemplares", "precoVenda", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Exemplares", "preco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exemplares", "preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Exemplares", "precoVenda");
            DropColumn("dbo.Exemplares", "precoCompra");
        }
    }
}

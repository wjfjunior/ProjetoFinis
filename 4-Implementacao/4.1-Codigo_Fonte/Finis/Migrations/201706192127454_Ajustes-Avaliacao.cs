namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustesAvaliacao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Avaliacoes", "creditoEspecial", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Avaliacoes", "creditoParcial", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Avaliacoes", "creditoParcial", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Avaliacoes", "creditoEspecial", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

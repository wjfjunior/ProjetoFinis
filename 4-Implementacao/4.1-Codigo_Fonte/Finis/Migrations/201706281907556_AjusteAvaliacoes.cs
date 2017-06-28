namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteAvaliacoes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avaliacoes", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avaliacoes", "status");
        }
    }
}

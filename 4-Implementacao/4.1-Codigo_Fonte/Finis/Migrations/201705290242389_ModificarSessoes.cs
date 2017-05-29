namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificarSessoes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sessoes", "subsessaoId", "dbo.Sessoes");
            DropIndex("dbo.Sessoes", new[] { "subsessaoId" });
            DropColumn("dbo.Sessoes", "subsessaoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessoes", "subsessaoId", c => c.Int());
            CreateIndex("dbo.Sessoes", "subsessaoId");
            AddForeignKey("dbo.Sessoes", "subsessaoId", "dbo.Sessoes", "id");
        }
    }
}

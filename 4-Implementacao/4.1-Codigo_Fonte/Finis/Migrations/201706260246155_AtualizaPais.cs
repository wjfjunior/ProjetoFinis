namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizaPais : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estados", "paisNome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Estados", "paisNome");
        }
    }
}

namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizaPais_2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Estados", "paisNome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Estados", "paisNome", c => c.String(nullable: false));
        }
    }
}

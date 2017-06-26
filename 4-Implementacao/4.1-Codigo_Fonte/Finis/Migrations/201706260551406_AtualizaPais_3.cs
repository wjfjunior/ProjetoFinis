namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizaPais_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Paises", "sigla", c => c.String(nullable: false, maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Paises", "sigla", c => c.String(nullable: false, maxLength: 2));
        }
    }
}

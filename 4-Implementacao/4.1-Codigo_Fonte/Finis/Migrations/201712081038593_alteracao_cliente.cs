namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteracao_cliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clientes", "ativo");
        }
    }
}

namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criar_usuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        email = c.String(maxLength: 50),
                        senha = c.String(nullable: false, maxLength: 8),
                        confirmaSenha = c.String(nullable: false, maxLength: 8),
                        ativo = c.Boolean(nullable: false),
                        pefil = c.Int(nullable: false),
                        nome = c.String(nullable: false),
                        sobrenome = c.String(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
        }
    }
}

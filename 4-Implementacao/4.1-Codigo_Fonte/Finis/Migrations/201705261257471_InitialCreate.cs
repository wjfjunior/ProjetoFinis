namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autores",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        exemplar_id = c.Int(),
                        nome = c.String(nullable: false, maxLength: 50),
                        email = c.String(maxLength: 50),
                        telefone = c.String(),
                        celular = c.String(),
                        enderecoId = c.Int(),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Enderecos", t => t.enderecoId)
                .ForeignKey("dbo.Exemplares", t => t.exemplar_id)
                .Index(t => t.exemplar_id)
                .Index(t => t.enderecoId);
            
            CreateTable(
                "dbo.Enderecos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        logradouro = c.String(maxLength: 30),
                        numero = c.Int(nullable: false),
                        complemento = c.String(maxLength: 30),
                        bairro = c.String(maxLength: 20),
                        cep = c.Int(),
                        cidadeId = c.Int(),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Cidades", t => t.cidadeId)
                .Index(t => t.cidadeId);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 30),
                        estadoId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Estados", t => t.estadoId, cascadeDelete: true)
                .Index(t => t.estadoId);
            
            CreateTable(
                "dbo.Estados",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 30),
                        sigla = c.String(nullable: false, maxLength: 2),
                        paisId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Paises", t => t.paisId, cascadeDelete: true)
                .Index(t => t.paisId);
            
            CreateTable(
                "dbo.Paises",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 30),
                        sigla = c.String(nullable: false, maxLength: 2),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Exemplares",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        titulo = c.String(nullable: false, maxLength: 30),
                        conservacao = c.Int(nullable: false),
                        ano = c.DateTime(nullable: false),
                        descricao = c.String(maxLength: 200),
                        vendaOnline = c.Boolean(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                        editora_id = c.Int(),
                        idioma_id = c.Int(),
                        sessao_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Fornecedores", t => t.editora_id)
                .ForeignKey("dbo.Idiomas", t => t.idioma_id)
                .ForeignKey("dbo.Sessoes", t => t.sessao_id)
                .Index(t => t.editora_id)
                .Index(t => t.idioma_id)
                .Index(t => t.sessao_id);
            
            CreateTable(
                "dbo.Fornecedores",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cnpj = c.String(maxLength: 50),
                        nome = c.String(nullable: false, maxLength: 50),
                        email = c.String(maxLength: 50),
                        telefone = c.String(),
                        celular = c.String(),
                        enderecoId = c.Int(),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Enderecos", t => t.enderecoId)
                .Index(t => t.enderecoId);
            
            CreateTable(
                "dbo.Idiomas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 20),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                        pais_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Paises", t => t.pais_id)
                .Index(t => t.pais_id);
            
            CreateTable(
                "dbo.Sessoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 20),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Avaliacoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        saldoCreditoParcial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        saldoCreditoEspecial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        dataNascimento = c.DateTime(nullable: false),
                        genero = c.Int(nullable: false),
                        rg = c.String(nullable: false, maxLength: 20),
                        nome = c.String(nullable: false, maxLength: 50),
                        email = c.String(maxLength: 50),
                        telefone = c.String(),
                        celular = c.String(),
                        enderecoId = c.Int(),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Enderecos", t => t.enderecoId)
                .Index(t => t.enderecoId);
            
            CreateTable(
                "dbo.Transacoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        data = c.DateTime(nullable: false),
                        tipoTransacao = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                        cliente_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clientes", t => t.cliente_id)
                .Index(t => t.cliente_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transacoes", "cliente_id", "dbo.Clientes");
            DropForeignKey("dbo.Fornecedores", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Clientes", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Exemplares", "sessao_id", "dbo.Sessoes");
            DropForeignKey("dbo.Exemplares", "idioma_id", "dbo.Idiomas");
            DropForeignKey("dbo.Idiomas", "pais_id", "dbo.Paises");
            DropForeignKey("dbo.Exemplares", "editora_id", "dbo.Fornecedores");
            DropForeignKey("dbo.Autores", "exemplar_id", "dbo.Exemplares");
            DropForeignKey("dbo.Autores", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Enderecos", "cidadeId", "dbo.Cidades");
            DropForeignKey("dbo.Cidades", "estadoId", "dbo.Estados");
            DropForeignKey("dbo.Estados", "paisId", "dbo.Paises");
            DropIndex("dbo.Transacoes", new[] { "cliente_id" });
            DropIndex("dbo.Clientes", new[] { "enderecoId" });
            DropIndex("dbo.Idiomas", new[] { "pais_id" });
            DropIndex("dbo.Fornecedores", new[] { "enderecoId" });
            DropIndex("dbo.Exemplares", new[] { "sessao_id" });
            DropIndex("dbo.Exemplares", new[] { "idioma_id" });
            DropIndex("dbo.Exemplares", new[] { "editora_id" });
            DropIndex("dbo.Estados", new[] { "paisId" });
            DropIndex("dbo.Cidades", new[] { "estadoId" });
            DropIndex("dbo.Enderecos", new[] { "cidadeId" });
            DropIndex("dbo.Autores", new[] { "enderecoId" });
            DropIndex("dbo.Autores", new[] { "exemplar_id" });
            DropTable("dbo.Transacoes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Avaliacoes");
            DropTable("dbo.Sessoes");
            DropTable("dbo.Idiomas");
            DropTable("dbo.Fornecedores");
            DropTable("dbo.Exemplares");
            DropTable("dbo.Paises");
            DropTable("dbo.Estados");
            DropTable("dbo.Cidades");
            DropTable("dbo.Enderecos");
            DropTable("dbo.Autores");
        }
    }
}

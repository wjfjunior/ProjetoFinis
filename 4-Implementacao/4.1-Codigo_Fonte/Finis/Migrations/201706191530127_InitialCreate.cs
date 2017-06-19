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
                        nome = c.String(nullable: false, maxLength: 50),
                        email = c.String(maxLength: 50),
                        telefone = c.String(maxLength: 15),
                        celular = c.String(maxLength: 15),
                        enderecoId = c.Int(),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                        Exemplar_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Enderecos", t => t.enderecoId)
                .ForeignKey("dbo.Exemplares", t => t.Exemplar_id)
                .Index(t => t.enderecoId)
                .Index(t => t.Exemplar_id);
            
            CreateTable(
                "dbo.Enderecos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        logradouro = c.String(maxLength: 30),
                        numero = c.Int(nullable: false),
                        complemento = c.String(maxLength: 30),
                        bairro = c.String(maxLength: 20),
                        cep = c.Int(nullable: false),
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
                "dbo.Avaliacoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dataEntrada = c.DateTime(nullable: false),
                        quantidadeExemplares = c.Int(nullable: false),
                        creditoEspecial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        creditoParcial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        situacao = c.Int(nullable: false),
                        observacao = c.String(maxLength: 200),
                        clienteId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clientes", t => t.clienteId, cascadeDelete: true)
                .Index(t => t.clienteId);
            
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
                        telefone = c.String(maxLength: 15),
                        celular = c.String(maxLength: 15),
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
                "dbo.Exemplares",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        titulo = c.String(nullable: false, maxLength: 30),
                        conservacao = c.Int(nullable: false),
                        isbn = c.String(maxLength: 32),
                        ano = c.Int(nullable: false),
                        edicao = c.Int(nullable: false),
                        precoCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        precoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        descricao = c.String(maxLength: 200),
                        peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        vendaOnline = c.Int(nullable: false),
                        quantidade = c.Int(nullable: false),
                        editoraId = c.Int(nullable: false),
                        idiomaId = c.Int(nullable: false),
                        sessaoId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Editoras", t => t.editoraId)
                .ForeignKey("dbo.Idiomas", t => t.idiomaId, cascadeDelete: true)
                .ForeignKey("dbo.Sessoes", t => t.sessaoId, cascadeDelete: true)
                .Index(t => t.editoraId)
                .Index(t => t.idiomaId)
                .Index(t => t.sessaoId);
            
            CreateTable(
                "dbo.Fornecedores",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(maxLength: 50),
                        cnpj = c.String(maxLength: 50),
                        telefone = c.String(maxLength: 15),
                        email = c.String(maxLength: 50),
                        tipoFornecedor = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Idiomas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 20),
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
                "dbo.Transacoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        data = c.DateTime(nullable: false),
                        tipoTransacao = c.Int(nullable: false),
                        tipoCredito = c.Int(nullable: false),
                        clienteId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(),
                        date_update = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clientes", t => t.clienteId, cascadeDelete: true)
                .Index(t => t.clienteId);
            
            CreateTable(
                "dbo.Editoras",
                c => new
                    {
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Fornecedores", t => t.id)
                .Index(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Editoras", "id", "dbo.Fornecedores");
            DropForeignKey("dbo.Transacoes", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Exemplares", "sessaoId", "dbo.Sessoes");
            DropForeignKey("dbo.Exemplares", "idiomaId", "dbo.Idiomas");
            DropForeignKey("dbo.Idiomas", "paisId", "dbo.Paises");
            DropForeignKey("dbo.Exemplares", "editoraId", "dbo.Editoras");
            DropForeignKey("dbo.Autores", "Exemplar_id", "dbo.Exemplares");
            DropForeignKey("dbo.Avaliacoes", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Clientes", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Autores", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Enderecos", "cidadeId", "dbo.Cidades");
            DropForeignKey("dbo.Cidades", "estadoId", "dbo.Estados");
            DropForeignKey("dbo.Estados", "paisId", "dbo.Paises");
            DropIndex("dbo.Editoras", new[] { "id" });
            DropIndex("dbo.Transacoes", new[] { "clienteId" });
            DropIndex("dbo.Idiomas", new[] { "paisId" });
            DropIndex("dbo.Exemplares", new[] { "sessaoId" });
            DropIndex("dbo.Exemplares", new[] { "idiomaId" });
            DropIndex("dbo.Exemplares", new[] { "editoraId" });
            DropIndex("dbo.Clientes", new[] { "enderecoId" });
            DropIndex("dbo.Avaliacoes", new[] { "clienteId" });
            DropIndex("dbo.Estados", new[] { "paisId" });
            DropIndex("dbo.Cidades", new[] { "estadoId" });
            DropIndex("dbo.Enderecos", new[] { "cidadeId" });
            DropIndex("dbo.Autores", new[] { "Exemplar_id" });
            DropIndex("dbo.Autores", new[] { "enderecoId" });
            DropTable("dbo.Editoras");
            DropTable("dbo.Transacoes");
            DropTable("dbo.Sessoes");
            DropTable("dbo.Idiomas");
            DropTable("dbo.Fornecedores");
            DropTable("dbo.Exemplares");
            DropTable("dbo.Clientes");
            DropTable("dbo.Avaliacoes");
            DropTable("dbo.Paises");
            DropTable("dbo.Estados");
            DropTable("dbo.Cidades");
            DropTable("dbo.Enderecos");
            DropTable("dbo.Autores");
        }
    }
}

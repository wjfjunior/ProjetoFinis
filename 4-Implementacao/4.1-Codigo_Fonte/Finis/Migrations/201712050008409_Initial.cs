namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        enderecoId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Enderecos", t => t.enderecoId, cascadeDelete: true)
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
                        cep = c.Int(nullable: false),
                        cidadeId = c.Int(),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
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
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
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
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
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
                        sigla = c.String(nullable: false, maxLength: 3),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Itens",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 50),
                        descricao = c.String(maxLength: 200),
                        precoCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        precoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        precoTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quantidade = c.Int(nullable: false),
                        estoqueMinimo = c.Int(nullable: false),
                        VendaId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                        conservacao = c.Int(),
                        isbn = c.String(maxLength: 32),
                        ano = c.Int(),
                        edicao = c.Int(),
                        peso = c.Decimal(precision: 18, scale: 2),
                        vendaOnline = c.Int(),
                        editoraId = c.Int(),
                        idiomaId = c.Int(),
                        sessaoId = c.Int(),
                        unidadeMedidaId = c.Int(),
                        marcaId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Pedido_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Fornecedores", t => t.editoraId, cascadeDelete: true)
                .ForeignKey("dbo.Idiomas", t => t.idiomaId, cascadeDelete: true)
                .ForeignKey("dbo.Sessoes", t => t.sessaoId, cascadeDelete: true)
                .ForeignKey("dbo.Vendas", t => t.VendaId, cascadeDelete: true)
                .ForeignKey("dbo.Marcas", t => t.marcaId, cascadeDelete: true)
                .ForeignKey("dbo.UnidadesMedida", t => t.unidadeMedidaId, cascadeDelete: true)
                .ForeignKey("dbo.Pedidos", t => t.Pedido_id)
                .Index(t => t.VendaId)
                .Index(t => t.editoraId)
                .Index(t => t.idiomaId)
                .Index(t => t.sessaoId)
                .Index(t => t.unidadeMedidaId)
                .Index(t => t.marcaId)
                .Index(t => t.Pedido_id);
            
            CreateTable(
                "dbo.Fornecedores",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(maxLength: 50),
                        cnpj = c.String(maxLength: 50),
                        telefone = c.String(maxLength: 15),
                        email = c.String(maxLength: 50),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
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
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dataCompra = c.DateTime(nullable: false),
                        creditoEspecial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        creditoParcial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        desconto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        descontoPorcentagem = c.Int(nullable: false),
                        subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        recebido = c.Decimal(nullable: false, precision: 18, scale: 2),
                        troco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 50),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UnidadesMedida",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        grandeza = c.String(nullable: false, maxLength: 50),
                        unidade = c.String(nullable: false, maxLength: 20),
                        simbolo = c.String(nullable: false, maxLength: 3),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Avaliacoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dataEntrada = c.DateTime(nullable: false),
                        quantidadeExemplares = c.Int(nullable: false),
                        creditoEspecial = c.Decimal(precision: 18, scale: 2),
                        creditoParcial = c.Decimal(precision: 18, scale: 2),
                        situacao = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        observacao = c.String(maxLength: 200),
                        clienteId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
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
                        enderecoId = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Enderecos", t => t.enderecoId, cascadeDelete: true)
                .Index(t => t.enderecoId);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        clienteId = c.Int(nullable: false),
                        descricao = c.String(maxLength: 200),
                        dataPedido = c.DateTime(nullable: false),
                        situacao = c.Int(nullable: false),
                        user_insert = c.String(),
                        user_update = c.String(),
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clientes", t => t.clienteId, cascadeDelete: true)
                .Index(t => t.clienteId);
            
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
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clientes", t => t.clienteId, cascadeDelete: true)
                .Index(t => t.clienteId);
            
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
                        date_insert = c.DateTime(nullable: false),
                        date_update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ExemplarAutors",
                c => new
                    {
                        Exemplar_id = c.Int(nullable: false),
                        Autor_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exemplar_id, t.Autor_id })
                .ForeignKey("dbo.Itens", t => t.Exemplar_id, cascadeDelete: true)
                .ForeignKey("dbo.Autores", t => t.Autor_id, cascadeDelete: true)
                .Index(t => t.Exemplar_id)
                .Index(t => t.Autor_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transacoes", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Itens", "Pedido_id", "dbo.Pedidos");
            DropForeignKey("dbo.Pedidos", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Avaliacoes", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Clientes", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Itens", "unidadeMedidaId", "dbo.UnidadesMedida");
            DropForeignKey("dbo.Itens", "marcaId", "dbo.Marcas");
            DropForeignKey("dbo.Itens", "VendaId", "dbo.Vendas");
            DropForeignKey("dbo.Itens", "sessaoId", "dbo.Sessoes");
            DropForeignKey("dbo.Itens", "idiomaId", "dbo.Idiomas");
            DropForeignKey("dbo.Idiomas", "paisId", "dbo.Paises");
            DropForeignKey("dbo.Itens", "editoraId", "dbo.Fornecedores");
            DropForeignKey("dbo.ExemplarAutors", "Autor_id", "dbo.Autores");
            DropForeignKey("dbo.ExemplarAutors", "Exemplar_id", "dbo.Itens");
            DropForeignKey("dbo.Autores", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Enderecos", "cidadeId", "dbo.Cidades");
            DropForeignKey("dbo.Cidades", "estadoId", "dbo.Estados");
            DropForeignKey("dbo.Estados", "paisId", "dbo.Paises");
            DropIndex("dbo.ExemplarAutors", new[] { "Autor_id" });
            DropIndex("dbo.ExemplarAutors", new[] { "Exemplar_id" });
            DropIndex("dbo.Transacoes", new[] { "clienteId" });
            DropIndex("dbo.Pedidos", new[] { "clienteId" });
            DropIndex("dbo.Clientes", new[] { "enderecoId" });
            DropIndex("dbo.Avaliacoes", new[] { "clienteId" });
            DropIndex("dbo.Idiomas", new[] { "paisId" });
            DropIndex("dbo.Itens", new[] { "Pedido_id" });
            DropIndex("dbo.Itens", new[] { "marcaId" });
            DropIndex("dbo.Itens", new[] { "unidadeMedidaId" });
            DropIndex("dbo.Itens", new[] { "sessaoId" });
            DropIndex("dbo.Itens", new[] { "idiomaId" });
            DropIndex("dbo.Itens", new[] { "editoraId" });
            DropIndex("dbo.Itens", new[] { "VendaId" });
            DropIndex("dbo.Estados", new[] { "paisId" });
            DropIndex("dbo.Cidades", new[] { "estadoId" });
            DropIndex("dbo.Enderecos", new[] { "cidadeId" });
            DropIndex("dbo.Autores", new[] { "enderecoId" });
            DropTable("dbo.ExemplarAutors");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Transacoes");
            DropTable("dbo.Pedidos");
            DropTable("dbo.Clientes");
            DropTable("dbo.Avaliacoes");
            DropTable("dbo.UnidadesMedida");
            DropTable("dbo.Marcas");
            DropTable("dbo.Vendas");
            DropTable("dbo.Sessoes");
            DropTable("dbo.Idiomas");
            DropTable("dbo.Fornecedores");
            DropTable("dbo.Itens");
            DropTable("dbo.Paises");
            DropTable("dbo.Estados");
            DropTable("dbo.Cidades");
            DropTable("dbo.Enderecos");
            DropTable("dbo.Autores");
        }
    }
}

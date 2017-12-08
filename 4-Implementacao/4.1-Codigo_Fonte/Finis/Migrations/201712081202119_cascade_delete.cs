namespace Finis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade_delete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Autores", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Cidades", "estadoId", "dbo.Estados");
            DropForeignKey("dbo.Estados", "paisId", "dbo.Paises");
            DropForeignKey("dbo.Itens", "editoraId", "dbo.Fornecedores");
            DropForeignKey("dbo.Itens", "idiomaId", "dbo.Idiomas");
            DropForeignKey("dbo.Itens", "sessaoId", "dbo.Sessoes");
            DropForeignKey("dbo.Idiomas", "paisId", "dbo.Paises");
            DropForeignKey("dbo.Avaliacoes", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Clientes", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Itens", "marcaId", "dbo.Marcas");
            DropForeignKey("dbo.Itens", "unidadeMedidaId", "dbo.UnidadesMedida");
            DropForeignKey("dbo.ItensVenda", "itemId", "dbo.Itens");
            DropForeignKey("dbo.ItensVenda", "vendaId", "dbo.Vendas");
            DropForeignKey("dbo.Vendas", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Pedidos", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Transacoes", "clienteId", "dbo.Clientes");
            AddForeignKey("dbo.Autores", "enderecoId", "dbo.Enderecos", "id");
            AddForeignKey("dbo.Cidades", "estadoId", "dbo.Estados", "id");
            AddForeignKey("dbo.Estados", "paisId", "dbo.Paises", "id");
            AddForeignKey("dbo.Itens", "editoraId", "dbo.Fornecedores", "id");
            AddForeignKey("dbo.Itens", "idiomaId", "dbo.Idiomas", "id");
            AddForeignKey("dbo.Itens", "sessaoId", "dbo.Sessoes", "id");
            AddForeignKey("dbo.Idiomas", "paisId", "dbo.Paises", "id");
            AddForeignKey("dbo.Avaliacoes", "clienteId", "dbo.Clientes", "id");
            AddForeignKey("dbo.Clientes", "enderecoId", "dbo.Enderecos", "id");
            AddForeignKey("dbo.Itens", "marcaId", "dbo.Marcas", "id");
            AddForeignKey("dbo.Itens", "unidadeMedidaId", "dbo.UnidadesMedida", "id");
            AddForeignKey("dbo.ItensVenda", "itemId", "dbo.Itens", "id");
            AddForeignKey("dbo.ItensVenda", "vendaId", "dbo.Vendas", "id");
            AddForeignKey("dbo.Vendas", "clienteId", "dbo.Clientes", "id");
            AddForeignKey("dbo.Pedidos", "clienteId", "dbo.Clientes", "id");
            AddForeignKey("dbo.Transacoes", "clienteId", "dbo.Clientes", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transacoes", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Pedidos", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Vendas", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.ItensVenda", "vendaId", "dbo.Vendas");
            DropForeignKey("dbo.ItensVenda", "itemId", "dbo.Itens");
            DropForeignKey("dbo.Itens", "unidadeMedidaId", "dbo.UnidadesMedida");
            DropForeignKey("dbo.Itens", "marcaId", "dbo.Marcas");
            DropForeignKey("dbo.Clientes", "enderecoId", "dbo.Enderecos");
            DropForeignKey("dbo.Avaliacoes", "clienteId", "dbo.Clientes");
            DropForeignKey("dbo.Idiomas", "paisId", "dbo.Paises");
            DropForeignKey("dbo.Itens", "sessaoId", "dbo.Sessoes");
            DropForeignKey("dbo.Itens", "idiomaId", "dbo.Idiomas");
            DropForeignKey("dbo.Itens", "editoraId", "dbo.Fornecedores");
            DropForeignKey("dbo.Estados", "paisId", "dbo.Paises");
            DropForeignKey("dbo.Cidades", "estadoId", "dbo.Estados");
            DropForeignKey("dbo.Autores", "enderecoId", "dbo.Enderecos");
            AddForeignKey("dbo.Transacoes", "clienteId", "dbo.Clientes", "id", cascadeDelete: true);
            AddForeignKey("dbo.Pedidos", "clienteId", "dbo.Clientes", "id", cascadeDelete: true);
            AddForeignKey("dbo.Vendas", "clienteId", "dbo.Clientes", "id", cascadeDelete: true);
            AddForeignKey("dbo.ItensVenda", "vendaId", "dbo.Vendas", "id", cascadeDelete: true);
            AddForeignKey("dbo.ItensVenda", "itemId", "dbo.Itens", "id", cascadeDelete: true);
            AddForeignKey("dbo.Itens", "unidadeMedidaId", "dbo.UnidadesMedida", "id", cascadeDelete: true);
            AddForeignKey("dbo.Itens", "marcaId", "dbo.Marcas", "id", cascadeDelete: true);
            AddForeignKey("dbo.Clientes", "enderecoId", "dbo.Enderecos", "id", cascadeDelete: true);
            AddForeignKey("dbo.Avaliacoes", "clienteId", "dbo.Clientes", "id", cascadeDelete: true);
            AddForeignKey("dbo.Idiomas", "paisId", "dbo.Paises", "id", cascadeDelete: true);
            AddForeignKey("dbo.Itens", "sessaoId", "dbo.Sessoes", "id", cascadeDelete: true);
            AddForeignKey("dbo.Itens", "idiomaId", "dbo.Idiomas", "id", cascadeDelete: true);
            AddForeignKey("dbo.Itens", "editoraId", "dbo.Fornecedores", "id", cascadeDelete: true);
            AddForeignKey("dbo.Estados", "paisId", "dbo.Paises", "id", cascadeDelete: true);
            AddForeignKey("dbo.Cidades", "estadoId", "dbo.Estados", "id", cascadeDelete: true);
            AddForeignKey("dbo.Autores", "enderecoId", "dbo.Enderecos", "id", cascadeDelete: true);
        }
    }
}

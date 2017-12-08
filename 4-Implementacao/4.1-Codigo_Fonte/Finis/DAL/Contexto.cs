using Finis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Finis.DAL
{
    public class Contexto : DbContext
    {
        public Contexto() : base("Contexto")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<Contexto>(null);
            //Database.SetInitializer<Contexto>(new DropCreateDatabaseAlways<Contexto>());
        }

        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
        public DbSet<UnidadeMedida> UnidadeMedida { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Idioma> Idioma { get; set; }
        public DbSet<Sessao> Sessao { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<ItemVenda> ItemVenda { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Avaliacao>().ToTable("Avaliacoes");
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");
            modelBuilder.Entity<Transacao>().ToTable("Transacoes");
            modelBuilder.Entity<Autor>().ToTable("Autores");
            modelBuilder.Entity<Pais>().ToTable("Paises");
            modelBuilder.Entity<Estado>().ToTable("Estados");
            modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            modelBuilder.Entity<Sessao>().ToTable("Sessoes");
            modelBuilder.Entity<UnidadeMedida>().ToTable("UnidadesMedida");
            modelBuilder.Entity<Marca>().ToTable("Marcas");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<Item>().ToTable("Itens");
            modelBuilder.Entity<Venda>().ToTable("Vendas");
            modelBuilder.Entity<ItemVenda>().ToTable("ItensVenda");

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
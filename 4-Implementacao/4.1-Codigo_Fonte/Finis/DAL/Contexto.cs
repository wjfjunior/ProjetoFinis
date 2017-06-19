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
            Database.SetInitializer<Contexto>(new DropCreateDatabaseIfModelChanges<Contexto>());
        }
        
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Exemplar> Exemplares { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Avaliacao>().ToTable("Avaliacoes");
            //modelBuilder.Entity<Exemplar>().ToTable("Exemplares");
            //modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");
            //modelBuilder.Entity<Transacao>().ToTable("Transacoes");
            //modelBuilder.Entity<Autor>().ToTable("Autores");
            //modelBuilder.Entity<Pais>().ToTable("Paises");
            //modelBuilder.Entity<Estado>().ToTable("Estados");
            //modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            //modelBuilder.Entity<Sessao>().ToTable("Sessoes");
            //modelBuilder.Entity<Editora>().ToTable("Editoras");
            //modelBuilder.Entity<Exemplar>().ToTable("Exemplares");
        } 
    }
}
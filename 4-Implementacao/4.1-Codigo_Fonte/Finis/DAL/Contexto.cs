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
        }

        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Exemplar> Exemplar { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Transacao> Transacao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avaliacao>().ToTable("Avaliacoes");
            modelBuilder.Entity<Exemplar>().ToTable("Exemplares");
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");
            modelBuilder.Entity<Transacao>().ToTable("Transacoes");
            modelBuilder.Entity<Autor>().ToTable("Autores");
            modelBuilder.Entity<Pais>().ToTable("Paises");
            modelBuilder.Entity<Estado>().ToTable("Estados");
            modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            modelBuilder.Entity<Sessao>().ToTable("Sessoes");
            modelBuilder.Entity<Editora>().ToTable("Editoras");

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Finis.Models.Idioma> Idiomas { get; set; }

        public System.Data.Entity.DbSet<Finis.Models.Sessao> Sessaos { get; set; }

        public System.Data.Entity.DbSet<Finis.Models.Editora> Fornecedors { get; set; }
    }
}
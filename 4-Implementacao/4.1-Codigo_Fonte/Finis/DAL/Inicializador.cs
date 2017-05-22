using Finis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finis.DAL
{
    public class Incializador : System.Data.Entity.DropCreateDatabaseIfModelChanges<Contexto>
    {
        protected override void Seed(Contexto contexto)
        {
            contexto.Database.ExecuteSqlCommand("ALTER TABLE clientes ALTER COLUMN nome NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CS_AI NOT NULL");

            var clientes = new List<Cliente>()
            {
                new Cliente {nome = "Wagner José Florencio Junior", email = "wagner@email.com", telefone = "(45)3693-5555", celular = "(45)99998-8888", dataNascimento = DateTime.Parse("1995-09-01"),
                    genero = TipoGenero.MASCULINO, rg = "102345678", saldoCreditoEspecial = 0.00M, saldoCreditoParcial = 0.00M,
                    endereco = new Endereco{logradouro = "Rua Artico", bairro = "Jd. Espanhola", cep = 85856440, complemento = "Ap 404", numero = 123,
                        cidade = new Cidade {nome = "Curitiba", estado = new Estado {nome = "Parana", sigla = "PR", pais = new Pais { nome = "Brasil", sigla = "BR" } } } }} };

            clientes.ForEach(c => contexto.Cliente.Add(c));
            contexto.SaveChanges();
        }
    }
}

//http://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx
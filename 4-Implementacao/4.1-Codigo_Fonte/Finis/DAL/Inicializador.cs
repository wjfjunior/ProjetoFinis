using Finis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finis.DAL
{
    public class Incializador : System.Data.Entity.CreateDatabaseIfNotExists<Contexto>
    {
        protected override void Seed(Contexto contexto)
        {
            var clientes = new List<Cliente>()
            {
                new Cliente {nome = "Wagner", email = "wagner@email.com", telefone = 33335555, celular = 999998888, dataNascimento = DateTime.Parse("1995-09-01"),
                    genero = TipoGenero.MASCULINO, rg = "102345678", saldoCreditoEspecial = 0.00M, saldoCreditoParcial = 0.00M,
                    endereco = new Endereco{ logradouro = "Rua Artico", bairro = "Jd. Espanhola", cep = 85856440, complemento = "Ap 404", numero = 123,
                        cidade = new Cidade { nome = "Arapuana", estado = new Estado { nome = "Parana", sigla = "PR", pais = new Pais { nome = "Brasil", sigla = "BR" } } } }} };

            clientes.ForEach(c => contexto.Cliente.Add(c));
            contexto.SaveChanges();
        }
    }
}

//http://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx
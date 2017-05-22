using Finis.Controllers;
using Finis.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Finis.Testes.Controllers
{
    [TestClass]
    class ClientesControllerTest
    {
        private Cliente InicializaCliente()
        {
            Cliente cliente = new Cliente
            {
                nome = "Wagner José Florencio Junior",
                email = "wagner@email.com",
                telefone = "(45)3693-5555",
                celular = "(45)99998-8888",
                dataNascimento = DateTime.Parse("1995-09-01"),
                genero = TipoGenero.MASCULINO,
                rg = "102345678",
                saldoCreditoEspecial = 6.00M,
                saldoCreditoParcial = 5.00M,
                endereco = new Endereco
                {
                    logradouro = "Avenida Brasil",
                    bairro = "Jd. Central",
                    cep = 85856440,
                    complemento = "Ap 404",
                    numero = 123,
                    cidade = new Cidade { nome = "Curitiba", estado = new Estado { nome = "Parana", sigla = "PR", pais = new Pais { nome = "Brasil", sigla = "BR" } } }
                }
            };

            return cliente;
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            ClientesController controller = new ClientesController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            ClientesController controller = new ClientesController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Save()
        {
            // Arrange
            ClientesController controller = new ClientesController();
            Exception exception = new Exception();
            Cliente cliente = this.InicializaCliente();

            // Act
            ViewResult result = controller.Create(cliente) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            ModelState modelState = result.ViewData.ModelState[""];
            Assert.IsNotNull(modelState);
            Assert.IsTrue(modelState.Errors.Any());
        }
        
        [TestMethod]
        public void Detalhes()
        {
            // Arrange
            ClientesController controller = new ClientesController();

            // Act
            JsonResult result = controller.Detalhes(1) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            ClientesController controller = new ClientesController();

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeletarRegistro()
        {
            // Arrange
            ClientesController controller = new ClientesController();

            // Act
            JsonResult result = controller.DeletarRegistro(1) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

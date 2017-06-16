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
    class ExemplaresControllerTest
    {
        private Exemplar InicializaExemplar()
        {
            Exemplar exemplar = new Exemplar
            {
                titulo = "As Crônicas de Nárnia - Volume Único",
                conservacao = Conservacao.NOVO,
                isbn = 85782798,
                ano = new DateTime(01 / 01 / 2009),
                edicao = 3,
                precoCompra = 16.00M,
                precoVenda = 23.00M,
                descricao = "Viagens ao fim do mundo, criaturas fantásticas e batalhas épicas entre o bem e o mal - o que mais um leitor poderia querer de um livro? " +
                "O livro que tem tudo isso é ''O leão, a feiticeira e o guarda-roupa'', escrito em 1949 por Clive Staples Lewis. Mas Lewis não parou por aí. Seis outros " +
                "livros vieram depois e, juntos, ficaram conhecidos como ''As crônicas de Nárnia''. ",
                peso = 1.00M,
                vendaOnline = true,
                quantidade = 28,
                editora = new Editora
                {
                    id = 1,
                    nome = "WMF Martins Fontes"
                },
                idioma = new Idioma
                {
                    id = 1,
                    nome = "Português",
                    pais = new Pais { id = 1, nome = "Brasil"}
                },
                sessao = new Sessao
                {
                    nome = "Infantil"
                }
            };

            return exemplar;
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }

        //Teste deve passar - SUCESSO
        [TestMethod]
        public void SalvarDevePassar()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();
            Exception exception = new Exception();
            Exemplar exemplar = this.InicializaExemplar();

            // Act
            ViewResult result = controller.Create(exemplar) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            ModelState modelState = result.ViewData.ModelState[""];
            Assert.IsNotNull(modelState);
            Assert.IsTrue(modelState.Errors.Any());
        }

        //Teste deve falhar - FALHA
        //O teste deve falhar pois o modelo de dados enviado a controller não está com os atributos requeridos preenchidos
        [TestMethod]
        public void SalvarDeveFalharValidacao()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();
            Exception exception = new Exception();
            Exemplar exemplar = new Exemplar();

            // Act
            ViewResult result = controller.Create(exemplar) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            ModelState modelState = result.ViewData.ModelState[""];
            Assert.IsNotNull(modelState);
            Assert.IsTrue(modelState.Errors.Any());
        }

        //Teste deve falhar - FALHA
        //O teste deve falhar pois o modelo de dados enviado a controller está com um dos atributos com valo máx de caracteres excedidos
        [TestMethod]
        public void SalvarDeveFalharValidacao2()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();
            Exception exception = new Exception();
            Exemplar exemplar = this.InicializaExemplar();
            exemplar.titulo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur volutpat ipsum eu ultrices congue. " +
                "Pellentesque congue sem vel odio posuere, in cursus neque molestie. Pellentesque facilisis sapien faucibus augue gravida porta.";

            // Act
            ViewResult result = controller.Create(exemplar) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            ModelState modelState = result.ViewData.ModelState[""];
            Assert.IsNotNull(modelState);
            Assert.IsTrue(modelState.Errors.Any());
        }

        //Teste deve passar - SUCESSO
        [TestMethod]
        public void DetalhesDevePassar()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();

            // Act
            JsonResult result = controller.Detalhes(1) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Teste deve falhar - FALHA
        //O teste deve falhar pois o id enviado ao método para exibir detalhes não existe
        [TestMethod]
        public void DetalhesDeveFalhar()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();

            // Act
            JsonResult result = controller.Detalhes(999) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Teste deve passar - SUCESSO
        [TestMethod]
        public void EditDevePassar()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Teste deve falhar - FALHA
        //O teste deve falhar pois o id enviado ao método para editar não existe
        [TestMethod]
        public void EditDeveFalhar()
        {
            // Arrange
            ExemplaresController controller = new ExemplaresController();

            // Act
            ViewResult result = controller.Edit(999) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Teste deve passar - SUCESSO
        [TestMethod]
        public void DeletarRegistroDevePassar()
        {
            // Arrange
            ClientesController controller = new ClientesController();

            // Act
            JsonResult result = controller.DeletarRegistro(1) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Teste deve falhar - FALHA
        //O teste deve falhar pois o id enviado ao método para deletar não existe
        [TestMethod]
        public void DeletarRegistroDeveFalharRegistroNaoEncontrado()
        {
            // Arrange
            ClientesController controller = new ClientesController();

            // Act
            JsonResult result = controller.DeletarRegistro(999) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

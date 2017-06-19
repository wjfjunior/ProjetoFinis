using Finis.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Finis.Testes.Controllers
{
    class AvaliacaoControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            AvaliacoesController controller = new AvaliacoesController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

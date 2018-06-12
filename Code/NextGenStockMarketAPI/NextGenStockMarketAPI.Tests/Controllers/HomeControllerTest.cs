using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextGenStockMarketAPI;
using NextGenStockMarketAPI.Controllers;

namespace NextGenStockMarketAPI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var  result = controller.Index();
            System.Console.WriteLine(result);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

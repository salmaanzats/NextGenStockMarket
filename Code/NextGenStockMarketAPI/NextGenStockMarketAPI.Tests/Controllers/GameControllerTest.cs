using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NextGenStockMarketAPI.Tests.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        [TestMethod]
        public void connectedPlayers()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.GameController();

            var result = controller.GetConnectedPlayersCount();

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void gameStatus()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.GameController();

            var result = controller.GameStatus();

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void getWinners()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.GameController();

            var result = controller.GetWinner();

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void newGame()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.GameController();

            var result = controller.NewGame();

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }
    }
}

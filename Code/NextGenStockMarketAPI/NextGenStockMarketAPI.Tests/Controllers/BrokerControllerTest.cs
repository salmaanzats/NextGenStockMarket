using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextGenStockMarket.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;
using System.Web.Http;

namespace NextGenStockMarketAPI.Tests.Controllers
{
    [TestClass]
    public class BrokerControllerTest
    {
        [TestMethod]
        public void createBroker()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BrokerController();

            var broker = DemoBroker();

            var result = controller.Create(broker.PlayerName);

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GetCompanies()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BrokerController();

            var result = controller.GetCompanies();

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GetSectors()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BrokerController();

            var result = controller.GetSectors("Google");

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Buy()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BrokerController();

            var broker = brokerInfo();

            var result = controller.Buy(broker);

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }
        [TestMethod]
        public void Sell()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BrokerController();

            var broker = brokerInfo();

            var result = controller.Sell(broker);

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Portfolio()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BrokerController();

            var broker = DemoBroker();

            var result = controller.GetPortfolio("Thathsarani");

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }


        BrokerAccount DemoBroker()
        {
            return new BrokerAccount { PlayerName = "Thathsarani" };
        }

        BrokerInfo brokerInfo()
        {
            return new BrokerInfo { PlayerName = "Thathsarani", Sector = "FInance", Quantity = 100, Status = "", StockPrice = 1000 };
        }
    }
}

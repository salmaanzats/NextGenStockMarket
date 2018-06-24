using System;
using NextGenStockMarket.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NextGenStockMarket.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace NextGenStockMarketAPI.Tests.Controllers
{
    [TestClass]
    public class BankControllerTest
    {
        [TestMethod]
        public void createAccount()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BankController();

            var account = DemoBankAccount();

            var result = controller.CreateAccount(account);

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public async Task CheckBankBalance()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BankController();

            var account = DemoBankAccount();

            IHttpActionResult actionResult = await controller.Get(account.PlayerName);
            var contentResult = actionResult as OkNegotiatedContentResult<String>;
            Console.WriteLine(contentResult.Content);

        }
        BankAccount DemoBankAccount()
        {
            return new BankAccount { PlayerName="Thathsarani" , Balance = 1000 };
        }
    }
}

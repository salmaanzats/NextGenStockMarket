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
        public void checkBankBalance()
        {
            var controller = new NextGenStockMarketAPI.Controllers.Api.BankController();

            var account = DemoBankAccount();

            var result = controller.Get(account.PlayerName); //as CreatedAtRouteNegotiatedContentResult<>;

            Assert.IsNotNull(result);
            Assert.Equals(result.ToString(), account.Balance);
            Console.WriteLine(result);
        }
      BankAccount DemoBankAccount()
        {
            return new BankAccount { PlayerName="Thathsarani" , Balance = 1000 };
        }
    }
}

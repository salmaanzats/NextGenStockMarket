using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service;
using NextGenStockMarket.Service.Interface;
using System.Threading.Tasks;

namespace NextGenStockMarketAPI.Tests.Controllers
{
    [TestClass]
    public class TestBankController
    {
        [TestMethod]
        public void CheckBankFunction()
        {
            var playerName = "Thathsarani";
//var bank = new BankController();
            var account = new BankAccount();
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var testAccounts = GetTestAccounts();
            var controller = new TestBankController(testAccounts);
            var result = controller.GetAllProducts() as String;

        }

        private System.Collections.Generic. GetTestAccounts()
        {
            var accounts = new String();
            accounts.Add(new Accounts { Name = "Demo1" });

            return accounts;
        }

    }
}

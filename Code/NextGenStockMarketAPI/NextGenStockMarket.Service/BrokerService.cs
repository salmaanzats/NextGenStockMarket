using Inx.CarWash.Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarket.Service
{
    public class BrokerService : IBrokerService
    {
        protected readonly ICacheManager cache;
        protected readonly IBankService bankService;

        public BrokerService(IBankService _bankService)
        {
            cache = new MemoryCacheManager();
            bankService = _bankService;
        }

        public async Task<BrokerAccount> CreateAccount(string playerName)
        {
            var Bank = cache.Get<AllBankRecords>(playerName + "_Bank");

            if (Bank == null)
            {
                throw new System.Exception("Bank account needs to be created first!");
            }

            var Broker = cache.Get<AllBankRecords>(playerName + "_Broker");
            if (Broker != null)
            {
                throw new System.Exception("Broker account has been already created for this user!");
            }

            BrokerAccount newPlayer = new BrokerAccount() { };
            newPlayer.PlayerName = playerName;

            var allBrokerData = new AllBrokerData() { };
            allBrokerData.Accounts = newPlayer;

            cache.Set(newPlayer.PlayerName + "_Broker", allBrokerData, Constants.cacheTime);
            return newPlayer;
        }

        public async Task<List<StockMarket>> GetCompany()
        {
            var companies = new List<StockMarket>();
            var markets = cache.Get<List<AllStockMarketRecords>>(Constants.marketData);

            if (markets == null)
            {
                throw new Exception("Stock market is empty");
            }

            foreach (var market in markets)
            {
                companies.Add(market.StockMarket);
            }

            return companies;
        }

        public async Task<List<Sector>> GetSector(string companyName)
        {
            var sectors = new List<Sector>();
            var markets = cache.Get<List<AllStockMarketRecords>>(Constants.marketData);

            if (markets == null)
            {
                throw new Exception("Stock market is empty");
            }

            foreach (var market in markets)
            {
                if (market.StockMarket.CompanyName == companyName)
                {
                    sectors = market.Sectors;
                }
            }
            return sectors;
        }

        public async Task<AllBrokerData> BuyStock(BrokerInfo brokerInfo)
        {
            var playerBrokerAccount = cache.Get<AllBrokerData>(brokerInfo.PlayerName + "_Broker");

            if (playerBrokerAccount == null)
            {
                throw new Exception("No broker account exists for provided player");
            }

            var playerBankAccount = cache.Get<AllBankRecords>(brokerInfo.PlayerName + "_Bank");
            var turn = cache.Get<Clock>(brokerInfo.PlayerName + "_Clock");
            var totalPrice = brokerInfo.StockPrice * brokerInfo.Quantity;

            if (playerBankAccount != null)
            {
                if (playerBankAccount.Accounts.Balance < totalPrice)
                {
                    throw new Exception("Player doesn't have enough money to buy");
                }
            }

            var bankTransaction = new BankTransaction();
            bankTransaction.PlayerName = playerBankAccount.Accounts.PlayerName;
            bankTransaction.Price = totalPrice;
            bankTransaction.Transceiver = brokerInfo.Sector;
            bankTransaction.Turn = turn.PlayerTurn + 1;
            await bankService.Withdraw(bankTransaction);

            var brokerRecords = new AllBrokerData();
            brokerRecords = playerBrokerAccount;
            brokerInfo.Status = Constants.boughtStock;
            brokerRecords.BrokerInfos.Add(brokerInfo);

            cache.Set(brokerInfo.PlayerName + "_Broker", brokerRecords, Constants.cacheTime);
            return brokerRecords;
        }

        public async Task<AllBrokerData> SellStock(BrokerInfo brokerInfo)
        {
            var playerBrokerAccount = cache.Get<AllBrokerData>(brokerInfo.PlayerName + "_Broker");

            if (playerBrokerAccount == null)
            {
                throw new Exception("No broker account exists for provided player");
            }

            var playerBankAccount = cache.Get<AllBankRecords>(brokerInfo.PlayerName + "_Bank");
            var turn = cache.Get<Clock>(brokerInfo.PlayerName + "_Clock");
            var totalPrice = brokerInfo.StockPrice * brokerInfo.Quantity;

            var bankTransaction = new BankTransaction();
            bankTransaction.PlayerName = playerBankAccount.Accounts.PlayerName;
            bankTransaction.Price = totalPrice;
            bankTransaction.Transceiver = brokerInfo.Sector;
            bankTransaction.Turn = turn.PlayerTurn + 1;
            await bankService.Deposit(bankTransaction);

            var brokerRecords = new AllBrokerData() { };
            brokerRecords = playerBrokerAccount;
            brokerInfo.Status = Constants.sellStock;
            brokerRecords.BrokerInfos.Add(brokerInfo);

            cache.Set(brokerInfo.PlayerName + "_Broker", brokerRecords, Constants.cacheTime);
            return brokerRecords;
        }

        public async Task<AllBrokerData> Portfolio(string playerName)
        {
            var playerBrokerAccount = cache.Get<AllBrokerData>(playerName + "_Broker");

            if (playerBrokerAccount == null)
            {
                throw new Exception("No broker account exists for provided data");
            }

            return playerBrokerAccount;
        }
    }
}

using Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;
using static NextGenStockMarket.Data.Entities.Clock;

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

        public async Task<List<string>> GetSectors()
        {
            var companies = new List<string>();
            var markets = cache.Get<List<AllStockMarketRecords>>(Constants.marketData);

            if (markets == null)
            {
                throw new Exception("Stock market is empty");
            }

            foreach (var market in markets)
            {
                companies.Add(market.StockMarket.CompanyName);
            }

            return companies;
        }

        public async Task<List<Sector>> GetStocks(string companyName)
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

        public void TurnChange()
        {
            Records records = new Records();
            if (cache.Get<Records>("Turn") == null)
            {
                records.Turns = 1;
                cache.Set("Turn", records, Constants.cacheTime);
            }
            else
            {
                records.Turns = cache.Get<Records>("Turn").Turns+1;
                cache.Remove("Turn");
                cache.Set("Turn", records, Constants.cacheTime);
            }
        }

        public async Task<AllBrokerData> BuyStock(BrokerInfo brokerInfo)
        {
            
            var stockbuy = new StockMarketService();
            var markets = cache.Get<List<AllStockMarketRecords>>(Constants.marketData);
            
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
            TurnChange();
            var CompaniesList = await GetStocks(brokerInfo.Stock);
            stockbuy.PriceUpdate(brokerInfo, CompaniesList, markets, "Buy");
            
            return brokerRecords;
        }

        public async Task<AllBrokerData> SellStock(BrokerInfo brokerInfo)
        {
            var stockbuy = new StockMarketService();
            var markets = cache.Get<List<AllStockMarketRecords>>(Constants.marketData);
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
            TurnChange();
            var CompaniesList = await GetStocks(brokerInfo.Stock);
            stockbuy.PriceUpdate(brokerInfo, CompaniesList, markets, "Sell");
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

        public async Task<List<ScoreArray>> AllData()
        {
            int turn = cache.Get<Records>("Turn").Turns;
            List<ScoreArray> data = new List<ScoreArray>(); ;
            var sector = await GetSectors();
            for(int i =1;i<=turn;i++)
            {
                foreach (var sec in sector)
                {
                    var comp = await GetStocks(sec);
                    foreach (var soc in comp)
                    {
                        string strlast = i + "_" + sec + "_" + soc.SectorName;
                        data.Add(cache.Get<ScoreArray>(strlast));
                    }
                }
            }

            if (data == null)
            {
                throw new Exception("No data");
            }

            return data;
        }
    }
}

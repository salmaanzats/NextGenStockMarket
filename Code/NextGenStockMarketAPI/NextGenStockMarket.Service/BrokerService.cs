using Inx.CarWash.Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarket.Service
{
    public class BrokerService: IBrokerService
    {
        protected readonly ICacheManager cache;

        public BrokerService()
        {
            cache = new MemoryCacheManager();
        }

        public async Task<BrokerAccount> CreateAccount(string playerName)
        {
            var Bank = cache.Get<AllBankRecords>(playerName);

            if (Bank == null)
            {
                throw new System.Exception("Bank account needs to be created first!");
            }
           
            BrokerAccount newPlayer = new BrokerAccount() { };
            newPlayer.PlayerName = playerName;
  

            var allBrokerData = new AllBrokerData() { };
            allBrokerData.Accounts = newPlayer;

            cache.Set(newPlayer.PlayerName, allBrokerData, Constants.cacheTime);
            return newPlayer;
        }
    }
}

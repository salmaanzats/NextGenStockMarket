using NextGenStockMarket.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarket.Service.Interface
{
    public interface IBrokerService
    {
        Task<BrokerAccount> CreateAccount(string playerName);
        Task<List<string>> GetSectors();
        Task<List<Sector>> GetStocks(string companyName);
        Task<AllBrokerData> SellStock(BrokerInfo brokerInfo);
        Task<AllBrokerData> BuyStock(BrokerInfo brokerInfo);
        Task<AllBrokerData> Portfolio(string playerName);
        Task<List<ScoreArray>> AllData();
    }
}

using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarket.Service.Interface
{
    public interface IBrokerService
    {
        Task<BrokerAccount> CreateAccount(string playerName);
    }
}

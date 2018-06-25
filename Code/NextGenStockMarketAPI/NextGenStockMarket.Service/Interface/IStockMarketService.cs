using NextGenStockMarket.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenStockMarket.Service.Interface
{
    public interface IStockMarketService
    {
        Task<List<AllStockMarketRecords>> GetMarketData();
        int GetPrice(string sector, string stock, int turn);
    }
}

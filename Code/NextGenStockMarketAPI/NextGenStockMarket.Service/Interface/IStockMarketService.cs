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
        Task<List<StockMarket>> getCompany();
        Task<List<Sector>> getSector(string companyName);
    }
}

using Inx.CarWash.Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextGenStockMarket.Service
{
    public class StockMarketService : IStockMarketService
    {
        protected readonly ICacheManager cache;

        public StockMarketService()
        {
            cache = new MemoryCacheManager();
        }

        public async Task<List<AllStockMarketRecords>> GetMarketData()
        {

            var allMarketData = new List<AllStockMarketRecords>();
            //First Market and sectors
            var marketOne = new AllStockMarketRecords();

            marketOne.StockMarket.CompanyName = "Google";
            marketOne.Sectors = new List<Sector>()
            {
                new Sector
                {
                    SectorName = "Financial",
                    StockPrice = 100
                },
                new Sector
                {
                    SectorName = "Artificial Intelligence",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Robotics",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Android",
                    StockPrice = 50
                }
            };
            allMarketData.Add(marketOne);
            //Second Market and sectors
            var marketTwo = new AllStockMarketRecords();
            marketTwo.StockMarket.CompanyName = "Google";
            marketTwo.Sectors = new List<Sector>()
            {
                new Sector
                {
                    SectorName = "Financial",
                    StockPrice = 100
                },
                new Sector
                {
                    SectorName = "Artificial Intelligence",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Robotics",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Android",
                    StockPrice = 50
                }
            };
            allMarketData.Add(marketTwo);
            //Third Market and sectors
            var marketThree = new AllStockMarketRecords();
            marketThree.StockMarket.CompanyName = "Google";
            marketThree.Sectors = new List<Sector>()
            {
                new Sector
                {
                    SectorName = "Financial",
                    StockPrice = 100
                },
                new Sector
                {
                    SectorName = "Artificial Intelligence",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Robotics",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Android",
                    StockPrice = 50
                }
            };
            allMarketData.Add(marketThree);

            //Fourth Market and sectors
            var marketFour = new AllStockMarketRecords();
            marketFour.StockMarket.CompanyName = "Google";
            marketFour.Sectors = new List<Sector>()
            {
                new Sector
                {
                    SectorName = "Financial",
                    StockPrice = 100
                },
                new Sector
                {
                    SectorName = "Artificial Intelligence",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Robotics",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Android",
                    StockPrice = 50
                }
            };
            allMarketData.Add(marketFour);
            return allMarketData;
        }
    }
}

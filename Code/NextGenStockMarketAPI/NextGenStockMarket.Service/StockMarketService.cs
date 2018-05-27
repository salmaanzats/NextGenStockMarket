using Inx.CarWash.Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
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
            var market = cache.Get<List<AllStockMarketRecords>>(Constants.marketData);

            if(market != null)
            {
                return market;
            }

            var allMarketData = new List<AllStockMarketRecords>();
            //First Market and sectors
            var marketOne = new AllStockMarketRecords();

            marketOne.StockMarket.CompanyName = "Google";
            marketOne.Sectors = new List<Sector>()
            {
                new Sector
                {
                    SectorName = "Google Financial",
                    StockPrice = 100
                },
                new Sector
                {
                    SectorName = "Google Artificial Intelligence",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Google Robotics",
                    StockPrice = 120
                },
                new Sector
                {
                    SectorName = "Google Android",
                    StockPrice = 50
                }
            };
            allMarketData.Add(marketOne);
            //Second Market and sectors
            var marketTwo = new AllStockMarketRecords();
            marketTwo.StockMarket.CompanyName = "Yahoo";
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
            marketThree.StockMarket.CompanyName = "Microsoft";
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
            marketFour.StockMarket.CompanyName = "Amazon";
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

            cache.Set(Constants.marketData, allMarketData, Constants.cacheTime);
            return allMarketData;
        }

        public async Task<List<StockMarket>> getCompany()
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

        public async Task<List<Sector>> getSector(string companyName)
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
                    sectors =market.Sectors;
                }
            }

            return sectors;
        }

    }
}

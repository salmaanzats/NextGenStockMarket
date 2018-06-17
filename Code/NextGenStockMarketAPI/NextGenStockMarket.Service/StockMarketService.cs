using Inx.CarWash.Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarket.Service
{
    public class StockMarketService : IStockMarketService
    {
        protected readonly ICacheManager cache;
        private IBrokerService brokerService;
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

        public void BuyStock(BrokerInfo brokerInfo, List<Sector> Sec,int turn,List<AllStockMarketRecords> AllData)
        {
            SectorTrend SectorTrend = new SectorTrend();
            ScoreArray ScoreArray = new ScoreArray();
            var match= Sec.Where(e => e.SectorName.Contains(brokerInfo.Sector)).FirstOrDefault();
            if(match!=null)
            {
                if (turn == 1)
                {
                    SectorTrend.SectorTrendValue = 1; 
                }
                else
                {
                    SectorTrend.SectorTrendValue = cache.Get<SectorTrend>(match.SectorName + "_ScoreArray").SectorTrendValue + 1; //GTValueChange(ScoreArray.GeneralTrend,"Buy");
                }
                cache.Set(match.SectorName + "_ScoreArray", SectorTrend, Constants.cacheTime);
            }       
            
            foreach(var Market in AllData)
            {
                foreach(var Stock in Market.Sectors)
                {
                    var TempValue = (cache.Get<SectorTrend>(Stock.SectorName + "_ScoreArray") != null) ? cache.Get<SectorTrend>(Stock.SectorName + "_ScoreArray").SectorTrendValue : 0;
                    //AllData.Where(e => e.StockMarket.CompanyName.Contains(brokerInfo.StockName)).FirstOrDefault()!=null
                    if (Market.StockMarket.CompanyName==brokerInfo.StockName && Stock.SectorName==brokerInfo.Sector )
                    {
                        if (turn == 1)
                        {
                            ScoreArray.GeneralTrend = 1; 
                        }
                        else
                        {
                            ScoreArray.GeneralTrend = cache.Get<ScoreArray>(Market.StockMarket.CompanyName + Market.Sectors + "_ScoreArray").GeneralTrend + 1;
                        }
                    }
                    else
                    {
                            ScoreArray.GeneralTrend = (cache.Get<ScoreArray>(Market.StockMarket.CompanyName + Market.Sectors + "_ScoreArray") != null) ? cache.Get<ScoreArray>(Market.StockMarket.CompanyName + Market.Sectors + "_ScoreArray").GeneralTrend : 0;
                    }
                    decimal percentage= calculateValue(PriceUpdate(TempValue, ScoreArray)) + 1;
                    decimal updatedprice = Stock.StockPrice * (percentage);
                    Market.Sectors.Where(w => w.SectorName == Stock.SectorName).ToList().ForEach(s => s.StockPrice = updatedprice);
                    cache.Set(Market.StockMarket.CompanyName + Market.Sectors + "_ScoreArray", ScoreArray, Constants.cacheTime);
                }
                
            }
        }

        private int GTValueChange(int generalTrend, string v)
        {
            if(v=="Buy")
            {
                return generalTrend++;
            }
            else
            {
                return generalTrend--;
            }
        }

        public ScoreArray PriceUpdate(int SectorTrendValue, ScoreArray ScoreArray)
        {
            ScoreArray.RandomMarket = GetRandomNumber();
            ScoreArray.SectorTrend = SectorTrendValue;
            return ScoreArray;
        }

        public decimal calculateValue(ScoreArray ScoreArray)
        {
            decimal sum = ScoreArray.RandomMarket + ScoreArray.SectorTrend + ScoreArray.GeneralTrend;
            decimal percentage = sum / 100;
            if (percentage > 0)
                return percentage;
            else
                return 0;
        }

        public int GetRandomNumber()
        {
            Random r = new Random();
            int rInt = r.Next(-2, 3);
            return rInt;
        }
    }
}

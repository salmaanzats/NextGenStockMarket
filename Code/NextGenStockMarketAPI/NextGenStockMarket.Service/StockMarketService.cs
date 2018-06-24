using Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarket.Service
{
    public class StockMarketService : IStockMarketService
    {
        Random r = new Random();
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
                    StockPrice = 60
                },
                new Sector
                {
                    SectorName = "Artificial Intelligence",
                    StockPrice = 45
                },
                new Sector
                {
                    SectorName = "Robotics",
                    StockPrice = 56
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
                    StockPrice = 26
                },
                new Sector
                {
                    SectorName = "Artificial Intelligence",
                    StockPrice = 15
                },
                new Sector
                {
                    SectorName = "Robotics",
                    StockPrice = 65
                },
                new Sector
                {
                    SectorName = "Android",
                    StockPrice = 45
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
                    StockPrice = 65
                },
                new Sector
                {
                    SectorName = "Artificial Intelligence",
                    StockPrice = 78
                },
                new Sector
                {
                    SectorName = "Robotics",
                    StockPrice = 98
                },
                new Sector
                {
                    SectorName = "Android",
                    StockPrice = 105
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
                    StockPrice = 26
                },
                new Sector
                {
                    SectorName = "Artificial Intelligence",
                    StockPrice = 89
                },
                new Sector
                {
                    SectorName = "Robotics",
                    StockPrice = 87
                },
                new Sector
                {
                    SectorName = "Android",
                    StockPrice = 36
                }
            };
            allMarketData.Add(marketFour);
            allMarketData = RandomMarket(allMarketData);
            cache.Set(Constants.marketData, allMarketData, Constants.cacheTime);
            //foreach(var item in allMarketData)
            //{
            //    foreach(var com in item.StockMarket.CompanyName)
            //    {
            //        foreach(var sec in item.Sectors)
            //        {
            //            item.Sectors.Where(w => w.SectorName == sec.SectorName).ToList().ForEach(s => s.StockPrice = GetRandomNumber(20,91));
            //        }
            //    }
            //}
            return allMarketData;
        }

        public List<AllStockMarketRecords> RandomMarket(List<AllStockMarketRecords> allMarketData)
        {
            foreach (var item in allMarketData)
            {
                foreach (var com in item.StockMarket.CompanyName)
                {
                    foreach (var sec in item.Sectors)
                    {
                        sec.StockPrice = GetRandomNumber(20, 91);
                    }
                }
            }
            return allMarketData;
        }

        public void PriceUpdate(BrokerInfo brokerInfo, List<Sector> Sec,List<AllStockMarketRecords> AllData,string Action)
        {
            int turn = cache.Get<Records>("Turn").Turns;
            bool Flag=false;
            SectorTrend SectorTrend = new SectorTrend();
            
            MainEventRecord mer = new MainEventRecord();
            MainEventRecord temp = new MainEventRecord();
            int sectortemp,stocktemp;
            var Event = EventGenerate(turn, calculatePreValue(turn));
            switch (Event.Type)
            {
                case "Stock":
                    {
                        mer.Stock = brokerInfo.Stock;
                        mer.Sector = brokerInfo.Sector;
                        mer.Value = (int)Event.EventSum;
                        break;
                    }                   
                case "Sector":
                    {
                        mer.Sector = brokerInfo.Sector;
                        mer.Stock = null;
                        mer.Value = (int)Event.EventSum;
                        break;
                    }
                case "Same":
                    {
                        temp = cache.Get<MainEventRecord>("MainEventRecord");
                        if(temp.Stock!=null)
                        {
                            stocktemp = temp.Value;
                        }
                        else
                        {
                            sectortemp = temp.Value;
                        }
                    }
                    break;
                case null:
                    {
                        mer.Sector = null;
                        mer.Stock = null;
                        mer.Value = (int)Event.EventSum;
                        break;
                    }
            }
            if (cache.Get<MainEventRecord>("MainEventRecord")  != null && Event.Type!="Same")
            {
                cache.Remove("MainEventRecord");
                cache.Set("MainEventRecord", mer, Constants.cacheTime);
            }
            else
            {
                cache.Set("MainEventRecord", mer, Constants.cacheTime);
            }
            var match= Sec.Where(e => e.SectorName.Contains(brokerInfo.Sector)).FirstOrDefault();
            if(match!=null)
            {
                if (turn == 1)
                {
                    var change = (Action == "Buy") ? 1 : -1;
                    SectorTrend.SectorTrendValue = change;
                    cache.Set(match.SectorName + "_ScoreArray", SectorTrend, Constants.cacheTime);
                }
                else
                {
                    var check = cache.Get<SectorTrend>(match.SectorName + "_ScoreArray");
                    var TempValue = (check != null) ? check.SectorTrendValue : 0;
                    SectorTrend.SectorTrendValue = GTValueChange(TempValue, Action);
                    cache.Remove(match.SectorName + "_ScoreArray");
                    cache.Set(match.SectorName + "_ScoreArray", SectorTrend, Constants.cacheTime);
                }
                
            }
            temp = cache.Get<MainEventRecord>("MainEventRecord");
            int last = turn - 1;
            foreach (var Market in AllData)
            {
                foreach(var Stock in Market.Sectors)
                {
                    ScoreArray ScoreArray = new ScoreArray();
                    var strnow = turn + "_" + Market.StockMarket.CompanyName + "_" + Stock.SectorName;
                    var strlast = last + "_" + Market.StockMarket.CompanyName + "_" + Stock.SectorName;

                    ScoreArray.EventComponent = (Stock.SectorName == temp.Sector && temp.Stock == null) ? temp.Value : 0;
                    ScoreArray.EventComponent = (Stock.SectorName == temp.Sector && Market.StockMarket.CompanyName == temp.Stock) ? temp.Value : 0;

                    var TempValue = (cache.Get<SectorTrend>(Stock.SectorName + "_ScoreArray") != null) ? cache.Get<SectorTrend>(Stock.SectorName + "_ScoreArray").SectorTrendValue : 0;
                    var check = cache.Get<ScoreArray>(strlast);
                    var GeneralTrend = (check != null) ? check.GeneralTrend : 0;
                    if (Market.StockMarket.CompanyName==brokerInfo.Stock && Stock.SectorName==brokerInfo.Sector )
                    {                       
                        Flag = true;
                        if (turn == 1)
                        {
                            var change = (Action == "Buy") ? 1 : -1;
                            ScoreArray.GeneralTrend = change;
                        }
                        else
                        {
                            ScoreArray.GeneralTrend = GTValueChange(GeneralTrend, Action);
                        }
                    }
                    else
                    {
                        ScoreArray.GeneralTrend = (cache.Get<ScoreArray>(strlast) != null) ? cache.Get<ScoreArray>(strlast).GeneralTrend : 0;
                    }

                    decimal percentage= calculateValue(PriceUpdate(TempValue, ScoreArray, Flag)) + 1;
                    decimal updatedprice = Stock.StockPrice * (percentage);

                    Market.Sectors.Where(w => w.SectorName == Stock.SectorName).ToList().ForEach(s => s.StockPrice = updatedprice);

                    if (cache.Get<ScoreArray>(strnow) == null)
                    {
                        cache.Set(strnow, ScoreArray, Constants.cacheTime);
                    }
                    else
                    {
                        cache.Remove(strnow);
                        cache.Set(strnow, ScoreArray, Constants.cacheTime);
                    }               
                }               
            }
        }

        
        public int calculatePreValue(int turn)
        {
            int last = turn - 1;
            if (turn == 1)
                return 0;
            else if (cache.Get<PrevVal>("Prev" + last) !=null && cache.Get<PrevVal>("Prev" + last).Value > 0 )
                return cache.Get<PrevVal>("Prev" + last).Value;
            else
                return 0;
        }

        private int GTValueChange(int value, string action)
        {
            int newval;
            var change = (action == "Buy") ? 1 : -1;
            newval = value + change;
            if (newval > 4)
                return 3;
            else if (newval<-3)
                return -3;
            else
                return newval;
        }

        public ScoreArray PriceUpdate(int SectorTrendValue, ScoreArray ScoreArray,bool? Flag)
        {
            int turn = cache.Get<Records>("Turn").Turns;
            ScoreArray.RandomMarket = RandomMarket();
            ScoreArray.SectorTrend = SectorTrendValue;
            PrevVal val = new PrevVal();
            if (Flag == true)
            {               
                val.Value = Sum(ScoreArray);
                cache.Set("Prev"+turn,val, Constants.cacheTime);
            }
            return ScoreArray;
        }

        public int Sum(ScoreArray ScoreArray)
        {
            int sum = ScoreArray.RandomMarket + ScoreArray.SectorTrend + ScoreArray.GeneralTrend;
            if (sum > 0)
                return sum;
            else
                return 0;
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

        public int RandomMarket()
        {
            int check = SelectEvent(50, 75);
            int val;
            if (check == 1) // 1=Not Change
            {
                val = 0;
            }
            else if (check == 2) // 2=Increase
            {
                val = GetRandomNumber(1, 3);
            }
            else // 3=Decrease
            {
                val = GetRandomNumber(-1, -3);
            }
            return val;
        }

        public int GetRandomNumber(int min, int max)
        {
            int rInt = r.Next(min, max);
            return rInt;
        }

        public Event EventGenerate(int CurrentTurn,int Sum)
        {
            int End = (CurrentTurn == 1)? 8 : 13; 
            double val = System.Convert.ToDouble(CurrentTurn) / 10;
            double MaxVal = Math.Ceiling(val)*11;
            Event EventStream = new Event();
            EventRecord Record = new EventRecord();
            if (CurrentTurn==1)
            {
                Record.LastTurn = 0;
                Record.NextTurn = GetRandomNumber(2, 11);
                cache.Set("_Event", Record, Constants.cacheTime);
                EventStream.EventSum = 0;
            }
            else if(CurrentTurn - cache.Get<EventRecord>("_Event").LastTurn < cache.Get<EventRecord>("_Event").Duration)
            {
                EventStream.EventSum = cache.Get<EventRecord>("_Event").EventSum;
                EventStream.Type = "Same";
            }

            else if (CurrentTurn - 1 == cache.Get<EventRecord>("_Event").LastTurn)
            {
                EventStream.EventSum = 0;
            }
            else if(cache.Get<EventRecord>("_Event").NextTurn== GetRandomNumber(CurrentTurn+1, (int)MaxVal) || CurrentTurn - cache.Get<EventRecord>("_Event").LastTurn>10)
            {
                if (SelectEvent(33, null) == 1) // 1=Sector 
                {
                    if (SelectEvent(50, null) == 1) //1=Boom 
                    {
                        EventStream.EventSum = Remap(Sum, 0, End, 1, 5);
                    }
                    else //3=Bust
                    {
                        EventStream.EventSum = Remap(Sum, 0, End, -5, -1);
                    }
                    Record.Duration = GetRandomNumber(2, 6);
                    EventStream.Type = "Sector";
                }
                else //3=Stock
                {
                    int check = SelectEvent(50, 75);
                    if (check == 1) // 1=PROFIT_WARNING
                    {
                        EventStream.EventSum = Remap(Sum, 0, End, 2, 3);
                    }
                    else if (check == 2) // 2=TAKE_OVER
                    {
                        EventStream.EventSum = Remap(Sum, 0, End, -5, -1);
                    }
                    else // 3=SCANDAL
                    {
                        EventStream.EventSum = Remap(Sum, 0, End, -5, -3);
                    }
                    Record.Duration = GetRandomNumber(1, 8);
                    EventStream.Type = "Stock";
                }
                Record.LastTurn = CurrentTurn;
                Record.NextTurn = GetRandomNumber(2, 11);
                Record.EventSum = EventStream.EventSum;
                cache.Remove("_Event");
                cache.Set("_Event", Record, Constants.cacheTime); 
            }
            else
            {
                EventStream.EventSum = 0;
            }
                          
            return EventStream;
        }

        public int SelectEvent(int Margin1 , int? Margin2)
        {
            Random RandomNumberGenerator = new Random();
            int[] percent = new int[100];
            for (int i = 0; i < 100; i++) 
            {
                if (i < Margin1)
                {
                    percent[i] = 1; 
                }
                else if (i < Margin1 + Margin2)
                {
                    percent[i] = 2; 
                }
                else
                {
                    percent[i] = 3;
                }
            }
            int SelectEvent = percent[RandomNumberGenerator.Next(0, 100)];

            return SelectEvent;
        }
        public static double Remap(float value, float from1, float to1, float from2, float to2)
        {
            double MapValue = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
            return Math.Round(MapValue, MidpointRounding.AwayFromZero);
        }
    }
}

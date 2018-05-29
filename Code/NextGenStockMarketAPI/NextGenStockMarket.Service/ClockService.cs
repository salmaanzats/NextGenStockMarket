using Inx.CarWash.Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System;

namespace NextGenStockMarket.Service
{
    public class ClockService : IClockService
    {
        protected readonly ICacheManager cache;
        protected readonly int Turn = 0;

        public ClockService()
        {
            cache = new MemoryCacheManager();
        }

        public void CreateClock(Clock clock)
        {
            var turn = cache.Get<Clock>(clock.PlayerName + "_Clock");
            if (turn != null)
            {
                throw new Exception("Player has already allocated clock");
            }
            cache.Set(clock.PlayerName + "_Clock", clock, Constants.cacheTime);
        }

        public string PlayerTurn(Clock clock)
        {
            var turn = cache.Get<Clock>(clock.PlayerName + "_Clock");
            turn.Turn += 1;

            if (turn.Turn == Constants.gameTurns)
            {
                return Constants.gameOver;
            }

            turn.PlayerTurn = clock.PlayerTurn;
           
            cache.Set(clock.PlayerName + "_Clock", turn, Constants.cacheTime);
            return Constants.play;
        }
    }
}

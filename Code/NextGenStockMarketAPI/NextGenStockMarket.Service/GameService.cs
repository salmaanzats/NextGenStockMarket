using Inx.CarWash.Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System;
using System.Collections.Generic;

namespace NextGenStockMarket.Service
{
    public class GameService : IGameService
    {
        protected readonly ICacheManager cache;

        public GameService(IClockService _clockService)
        {
            cache = new MemoryCacheManager();
        }

        public string CreatePlayer(Players player)
        {
            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");

            if (connectedPlayers != null)
            {
                foreach (var p in connectedPlayers.Players)
                {
                    if (p.PlayerName == player.PlayerName)
                    {
                        throw new Exception("Player Name already exists.Please try a different player name");
                    }
                }

                if (connectedPlayers.Players.Count == Constants.maximumPlayers)
                {
                    throw new Exception("Can't create account. Maximum players are connected!");
                }
                else
                {
                    connectedPlayers.Players.Add(player);
                    cache.Set("ConnectedPlayers", connectedPlayers, Constants.cacheTime);
                    return "Created";
                }
            }
            else
            {
                connectedPlayers = new AllPlayers();
                connectedPlayers.Players.Add(player);
                cache.Set("ConnectedPlayers", connectedPlayers, Constants.cacheTime);
                return "Created";
            }
        }
    }
}

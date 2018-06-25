using NextGenStockMarket.Data.Entities;
using System.Threading.Tasks;

namespace NextGenStockMarket.Service.Interface
{
    public interface IGameService
    {
        string CreatePlayer(Players player);
        Task<int> GetConnectedPlayers();
        Task<string> GameStatus();
        Task<string> GetWinner();
        Task<int> NewGame();
    }
}

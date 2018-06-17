using NextGenStockMarket.Service.Interface;
using System.Threading.Tasks;
using System.Web.Http;

namespace NextGenStockMarketAPI.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class GameController : ApiController
    {
        private IGameService gameService;

        public GameController(IGameService _gameService)
        {
            gameService = _gameService;
        }

        [HttpGet, Route("game/connectedplayers")]
        public async Task<IHttpActionResult> GetConnectedPlayersCount()
        {
            return Ok(gameService.GetConnectedPlayers());
        }

        [HttpGet, Route("game/gamestatus")]
        public async Task<IHttpActionResult> GameStatus()
        {
            return Ok(gameService.GameStatus());
        }

        [HttpGet, Route("game/getwinner")]
        public async Task<IHttpActionResult> GetWinner()
        {
            return Ok(gameService.GetWinner());
        }

        [HttpGet, Route("game/newgame")]
        public async Task<IHttpActionResult> NewGame()
        {
           return Ok(gameService.NewGame());
        }

    }
}
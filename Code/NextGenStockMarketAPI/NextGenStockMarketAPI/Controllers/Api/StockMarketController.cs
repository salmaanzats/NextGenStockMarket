using NextGenStockMarket.Service.Interface;
using System.Threading.Tasks;
using System.Web.Http;

namespace NextGenStockMarketAPI.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class StockMarketController : ApiController
    {
        private IStockMarketService stockMarketService;

        public StockMarketController(IStockMarketService _stockMarketService)
        {
            stockMarketService = _stockMarketService;
        }

        [HttpGet, Route("market/stock")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await stockMarketService.GetMarketData());
        }

        [HttpGet, Route("market/getprice")]
        public int GetPrice(string sector, string stock, int turn)
        {
            return stockMarketService.GetPrice(sector, stock, turn);
        }
    }
}
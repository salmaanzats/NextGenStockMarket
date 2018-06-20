using NextGenStockMarket.Service.Interface;
using System.Threading.Tasks;
using System.Web.Http;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarketAPI.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class BrokerController : ApiController
    {
        private IBrokerService brokerService;

        public BrokerController(IBrokerService _brokerService)
        {
            brokerService = _brokerService;
        }

        public BrokerController()
        {
        }

        [HttpPost, Route("broker/createaccount")]
        public async Task<IHttpActionResult> Create(string playerName)
        {
            return Ok(await brokerService.CreateAccount(playerName));
        }


        [HttpGet, Route("broker/getsectors")]
        public async Task<IHttpActionResult> GetCompanies()
        {
            return Ok(await brokerService.GetSectors());
        }

        [HttpGet, Route("broker/getstocks")]
        public async Task<IHttpActionResult> GetStocks(string companyName)
        {
            return Ok(await brokerService.GetStocks(companyName));
        }

        [HttpPost, Route("broker/buy")]
        public async Task<IHttpActionResult> Buy(BrokerInfo brokerInfo)
        {
            return Ok(await brokerService.BuyStock(brokerInfo));
        }

        [HttpPost, Route("broker/sell")]
        public async Task<IHttpActionResult> Sell(BrokerInfo brokerInfo)
        {
            return Ok(await brokerService.SellStock(brokerInfo));
        }

        [HttpGet, Route("broker/portfolio")]
        public async Task<IHttpActionResult> GetPortfolio(string playerName)
        {
            return Ok(await brokerService.Portfolio(playerName));
        }
    }
}
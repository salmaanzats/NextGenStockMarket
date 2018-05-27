using NextGenStockMarket.Service.Interface;
using System.Threading.Tasks;
using System.Web.Http;

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

        [HttpPost, Route("createaccount")]
        public async Task<IHttpActionResult> Create(string playerName)
        {
            return Ok(await brokerService.CreateAccount(playerName));
        }

    }
}
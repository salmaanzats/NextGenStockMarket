using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace NextGenStockMarketAPI.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class BankController : ApiController
    {
        private IBankService bankService;

        public BankController() { }

        public BankController(IBankService _bankService)
        {
            bankService = _bankService;
        }

        [HttpPost, Route("bank/createaccount")]
        public async Task<IHttpActionResult> CreateAccount([FromBody]BankAccount bankAccount)
        {
            return Ok(await bankService.CreateBankAccount(bankAccount));
        }

        [HttpGet, Route("bank/bankbalance")]
        public async Task<IHttpActionResult> Get(string playerName)
        {
            return Ok(await bankService.ShowBankBalance(playerName));
        }

        [HttpPut, Route("bank/deposit")]
        public async Task<IHttpActionResult> Deposit(BankTransaction transaction)
        {
            return Ok(await bankService.Deposit(transaction));
        }

        [HttpPut, Route("bank/withdraw")]
        public async Task<IHttpActionResult> Withdraw(BankTransaction transaction)
        {
            return Ok(await bankService.Withdraw(transaction));
        }

        [HttpGet, Route("bank/getaccount")]
        public async Task<IHttpActionResult> GetAccount(string playerName)
        {
            return Ok(await bankService.GetBankAccount(playerName));
        }
    }
}
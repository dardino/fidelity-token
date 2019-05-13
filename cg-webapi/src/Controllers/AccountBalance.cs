using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nethereum.Web3;

namespace cg_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountBalanceController: ControllerBase
    {
        private Model.Nodes config;
        public AccountBalanceController(IOptions<Model.Nodes> config) => this.config = config.Value;

        [HttpGet]
        [Route("{wallet}")]
        public async Task<ActionResult> Get(string wallet)
        {
            var ipToTry = config.bootstrap.Ip;

            Console.WriteLine($"Ip Setting: {ipToTry}");
            wallet = "0x" + wallet;
            Console.WriteLine($"Wallet: {wallet}");
            var web3 = new Web3(ipToTry);
            
            try {
                var balance = await web3.Eth.GetBalance.SendRequestAsync(wallet);
                Console.WriteLine($"Balance in Wei: {balance.Value}");

                var etherAmount = Web3.Convert.FromWei(balance.Value);
                Console.WriteLine($"Balance in Ether: {etherAmount}");

                return Ok(new { ipToTry, balance.Value, etherAmount });
            } catch (HttpRequestException ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [Route("{wallet}/ismining")]
        public async Task<ActionResult> IsMining(string wallet)
        {
            var ipToTry = config.bootstrap.Ip;

            Console.WriteLine($"Ip Setting: {ipToTry}");
            wallet = "0x" + wallet;
            Console.WriteLine($"Wallet: {wallet}");
            var web3 = new Web3(ipToTry);
            
            try {
                var ismining = web3.Eth.Mining.IsMining;
                var x = await ismining.SendRequestAsync();
                Console.WriteLine($"is mining: {x}");

                return Ok(new { IsMining = x });
            } catch (HttpRequestException ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [Route("{wallet}/AddCredit")]
        public async Task<ActionResult> SmartContract(string wallet)
        {
            var ftSorucePath = System.IO.Path.Combine(Startup.ContentRootPath, "Content", "SmartContracts", "fidelityToken.sol");
            var source = await System.IO.File.ReadAllTextAsync(ftSorucePath);

            var ipToTry = config.bootstrap.Ip;
            Console.WriteLine($"Ip Setting: {ipToTry}");
            wallet = "0x" + wallet;
            Console.WriteLine($"Wallet: {wallet}");
            var web3 = new Web3(ipToTry);
            var compiled = await web3.Eth.Compile.CompileSolidity.SendRequestAsync(source);

            return Ok(compiled);
        }
    }
}
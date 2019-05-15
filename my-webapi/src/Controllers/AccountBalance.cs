using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nethereum.Web3;

namespace my_webapi.Controllers
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
            var abiSorucePath = System.IO.Path.Combine(Startup.ContentRootPath, "Content", "SmartContracts", "fidelityToken.abi");
            var abi = await System.IO.File.ReadAllTextAsync(abiSorucePath);
            var binSorucePath = System.IO.Path.Combine(Startup.ContentRootPath, "Content", "SmartContracts", "fidelityToken.bin");
            var byteCode = await System.IO.File.ReadAllTextAsync(binSorucePath);
            var outHashPath = System.IO.Path.Combine(Startup.ContentRootPath, "Content", "SmartContracts", "fidelityToken.hashes");

            var ipToTry = config.bootstrap.Ip;
            Console.WriteLine($"Ip Setting: {ipToTry}");
            wallet = "0x" + wallet;
            Console.WriteLine($"Wallet: {wallet}");
            var web3 = new Web3(ipToTry);

            bool unlockResult = false;
            try {
                unlockResult = await web3.Personal.UnlockAccount.SendRequestAsync(wallet, "", 120);
            } catch(Exception err) {
                Console.WriteLine(err.ToString());
                throw;
            }
            if (unlockResult) {
                var gas = new Nethereum.Hex.HexTypes.HexBigInteger(300000);
                var balance = new Nethereum.Hex.HexTypes.HexBigInteger(120);
                var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, wallet, gas, balance);
                if (!System.IO.File.Exists(outHashPath)) 
                    await System.IO.File.WriteAllLinesAsync(outHashPath, new List<String> { wallet + "\t" + transactionHash });
                else
                    await System.IO.File.AppendAllLinesAsync(outHashPath, new List<String> { wallet + "\t" + transactionHash });
                return Ok(transactionHash);
            } else {
                return NotFound("unlock non riuscito");
            }

            // var getAddress = "./geth.ipc";
            // var web3 = new Web3(ipcClient);
            // var reciept = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            // // If reciept is null it means the Contract creation transaction is not minded yet.
            // if (reciept != null)
            //     contractAddress = reciept.ContractAddress;        
        }
    }
}
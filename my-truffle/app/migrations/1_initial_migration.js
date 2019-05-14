const Web3 = require('web3');
const TruffleConfig = require('../truffle-config');
const Migrations = artifacts.require("Migrations");

module.exports = function(deployer, network, addresses) {
  const config = TruffleConfig.networks[network];

  if (process.env.ACCOUNT_PASSWORD) {
    const provUri = 'http://' + config.host + ':' + config.port;
    console.log("Provider URL: ", provUri);
    const web3 = new Web3(new Web3.providers.HttpProvider(provUri));

    console.log('>> Unlocking account ' + config.from);
    web3.personal.unlockAccount(config.from, process.env.ACCOUNT_PASSWORD, 36000);
  }

  console.log('>> Deploying migration');
  deployer.deploy(Migrations);
};

module.exports = {
  networks: {
    development: {
      host: "172.21.0.3",
      port: 8545,
      network_id: "*",
      from: "0x007ccffb7916f37f7aeef05e8096ecfbe55afc2f", // account che fa il deploy dello smart contract
      gas: 20000000
    }
  },
  rpc: {
    host: "172.21.0.3",
    port: 8543
  }
};

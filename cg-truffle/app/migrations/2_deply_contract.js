const FidelityToken = artifacts.require("FidelityToken");
module.exports = function(deployer) {
  deployer.deploy(FidelityToken, "0x007CcfFb7916F37F7AEEf05E8096ecFbe55AFc2f");
};
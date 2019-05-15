pragma solidity >=0.4.21 <0.6.0;

// guardare questo esempio:
// https://truffleframework.com/docs/truffle/getting-started/creating-a-project

contract FidelityToken {
    // id del coniatore visibile dall'esterno
    address public minter;
    // map tra account xxx e quantità di coin
    mapping (address => uint) public balances;
    // evento che notifica all'esterno una avvenuta transazione
    event Sent(address from, address to, uint amount);
    // evento che notifica all'esterno una avvenuto accredito di token
    event Credited(address from, address to, uint amount);
    // costruttore dello smart contract
    constructor() public {
        // imposto il coniatore con l'id dell'account che genera questo smart contract
        minter = msg.sender;
    }

    // metodo che assegna un ammontare di token ad un determinato account
    function mint(address receiver, uint amount) public {
        // solo l'account che ha generato lo smart contract è in grado di aggiungere token ad un account
        if(msg.sender != minter) return;
        // aggiungo l'ammontare di token
        balances[receiver] += amount;
        // emetto l'avvenuto accredito
        emit Sent(msg.sender, receiver, amount);
    }

    // metodo per l'interscambio di token
    function send(address receiver, uint amount) public {
        // se colui che sta chiamando questo metodo ha nel suo account un ammontare
        // di token inferiore a quello che vuole trasferire allora non faccio la transazione
        if(balances[msg.sender] < amount) return;

        balances[msg.sender] -= amount;
        balances[receiver] += amount;

        // emetto l'avvenuta transazione
        emit Sent(msg.sender, receiver, amount);
    }

    // restituisce quanti punti fedeltà ha una determinata tessera
    function getBalance(address account) public view returns(uint) {
        return balances[account];
    }
}
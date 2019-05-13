
# Introduction 
Questo progetto vuole essere un esempio su come costruire una infrastuttura con docker-compose per una rete Ethereum privata ed effettaure un deploy di uno smart contract per la gestione di "punti fedeltà" per account.

# Getting Started
## 1.	Installazione
- Clonare il repository con `git clone https://github.com/dardino/fidelity-token.git fidelity-token`
## 2.	Software dependencies
- Per far funzionare tutto è necessario aver installato [Docker](https://docs.docker.com/install/)

# Build and Test
## esempio che gira in automatico
- Eseguire da Powershell lo script `.\start.ps1`
## per fare sviluppo e test
- Eseguire da Powershell lo script `.\start.ps1 -dev [-scale:n] [-truffle]`
  spiegazione degli argomenti: 
  - `-dev` usa il file compose specifico per il dev.
  - `-scale:n` scala i nodi eth a 'n' istanze
  - `-truffle` inizializza una vm docker interattiva per poter utilizzare truffle


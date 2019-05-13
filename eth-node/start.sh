#!/bin/bash
echo --------------- START -----------------------
echo dos2unix files...
dos2unix /root/genesis/genesis.json
echo dos2unix done.
sleep 3
set -e
cd /root/eth-net-intelligence-api
perl -pi -e "s/XXX/$(hostname)/g" app.json
/usr/bin/pm2 start ./app.json
sleep 3
geth --datadir=~/.ethereum/devchain init /root/genesis/genesis.json
sleep 3
BOOTSTRAP_IP=`getent hosts bootstrap | cut -d" " -f1`
GETH_OPTS=${@/XXX/$BOOTSTRAP_IP}
geth $GETH_OPTS

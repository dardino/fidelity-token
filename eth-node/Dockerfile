FROM ethereum/client-go

RUN apk add --update git bash nodejs nodejs-npm perl
RUN apk add --no-cache bash coreutils grep sed

RUN cd /root &&\
    git clone https://github.com/cubedro/eth-net-intelligence-api &&\
    cd eth-net-intelligence-api &&\
    npm install &&\
    npm install -g pm2

COPY start.sh /root/start.sh
COPY eth-files/password /root/files/password
COPY eth-files/keystore /root/.ethereum/devchain/keystore
COPY eth-files/genesis /root/genesis
COPY app.json /root/eth-net-intelligence-api/app.json

RUN dos2unix /root/start.sh &&\
    dos2unix /root/genesis/genesis.json &&\
    dos2unix /root/eth-net-intelligence-api/app.json &&\
    chmod +x /root/start.sh

ENTRYPOINT /root/start.sh

RUN ls /root/
FROM desmart/truffle:latest

# FROM node:alpine
#
# RUN apk add python g++ make
# RUN npm i -g truffle
#
RUN mkdir -p /usr/src/app
# RUN su -
# RUN npm i web3
#
WORKDIR /usr/src/app
COPY ./app /usr/src/app
#
#
RUN dos2unix /usr/src/app/start.sh \
 && chmod +x /usr/src/app/start.sh
#
ENTRYPOINT /usr/src/app/start.sh


FROM node:alpine

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

RUN apk update
RUN apk add -t .gyp --no-cache git python g++ make \
 && apk upgrade \
 && apk del .gyp \
 && npm install -g truffle 

COPY ./app /usr/src/app

RUN dos2unix /usr/src/app/start.sh \
 && chmod +x /usr/src/app/start.sh

ENTRYPOINT /usr/src/app/start.sh


# base image
FROM node:alpine

RUN apk add bash python
RUN apk update
RUN apk upgrade
RUN mkdir -p /usr/src/app
RUN npm i -g @angular/cli

# set working directory
WORKDIR /usr/src/app

# copy app
COPY ./wallet-app /usr/src/app
# copy scripts
COPY ./start.sh /usr/src/start.sh

# npm install
RUN find /usr/src/app -type f -print0 | xargs -0 dos2unix &&\
    dos2unix /usr/src/start.sh                            &&\
    chmod +x /usr/src/start.sh                            &&\
    mkdir /usr/src/node_modules                           &&\
    ln -s /usr/src/node_modules /usr/src/app/node_modules

RUN npm install
RUN ng build

# start app
ENTRYPOINT /usr/src/start.sh

CMD serve --host 0.0.0.0


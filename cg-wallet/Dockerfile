# base image
FROM node:alpine

# install chrome for protractor tests
# RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
# RUN sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
# RUN apt-get update && apt-get install -yq google-chrome-stable

# set working directory
RUN mkdir /root/app &&\
    apk add bash

WORKDIR /root/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /root/app/node_modules/.bin:$PATH

# copy app
COPY ./wallet-app /root/app
# copy scripts
COPY ./start.sh /root/start.sh

# npm install
RUN find /root/app -type f -print0 | xargs -0 dos2unix &&\
    dos2unix /root/start.sh                            &&\
    chmod 777 /root/start.sh                           &&\
    chmod +x /root/start.sh                            &&\
    npm install -g @angular/cli                        &&\
    mkdir /root/node_modules                           &&\
    ln -s /root/node_modules /root/app/node_modules    &&\
    npm install

# start app
ENTRYPOINT /root/start.sh

CMD serve --host 0.0.0.0


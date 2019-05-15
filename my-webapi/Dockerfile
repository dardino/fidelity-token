FROM microsoft/dotnet:latest as build

RUN mkdir app

# copy csproj and restore as distinct layers
COPY ./src /app
WORKDIR /app
RUN dotnet restore
# RUN dotnet publish -c Release -o out
ENTRYPOINT [ "dotnet" ]
CMD [ "run" ]

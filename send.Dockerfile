#Build Stage
FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /workdir

COPY ./Send ./Send/

RUN dotnet restore ./Send/Send.csproj
RUN dotnet publish ./Send/Send.csproj -c Release -o /publish

FROM microsoft/dotnet:2.2-runtime
COPY --from=build-env /publish /publish
WORKDIR /publish
ENTRYPOINT ["dotnet", "Send.dll"]
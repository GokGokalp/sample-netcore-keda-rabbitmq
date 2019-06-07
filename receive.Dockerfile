#Build Stage
FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /workdir

COPY ./Receive ./Receive/

RUN dotnet restore ./Receive/Receive.csproj
RUN dotnet publish ./Receive/Receive.csproj -c Release -o /publish

FROM microsoft/dotnet:2.2-runtime
COPY --from=build-env /publish /publish
WORKDIR /publish
ENTRYPOINT ["dotnet", "Receive.dll"]
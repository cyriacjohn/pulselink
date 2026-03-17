FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY BDMS.sln .
COPY BDMS.Api/ BDMS.Api/
COPY BDMS.Application/ BDMS.Application/
COPY BDMS.Domain/ BDMS.Domain/
COPY BDMS.Infrastructure/ BDMS.Infrastructure/

RUN dotnet restore BDMS.Api/BDMS.Api.csproj

RUN dotnet publish BDMS.Api/BDMS.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BDMS.Api.dll"]
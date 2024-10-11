FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /BankPrikoloff

COPY ["BankPrikoloff/BankPrikoloff.csproj", "BankPrikoloff/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Domain/Domain.csproj", "Domain/"]

RUN dotnet restore "BankPrikoloff/BankPrikoloff.csproj"

COPY . .
FROM build AS publish
RUN dotnet publish "BankPrikoloff/BankPrikoloff.csproj" -c Release -o /app/publish /p:UserAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankPrikoloff.dll"]




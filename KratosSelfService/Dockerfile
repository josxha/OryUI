FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KratosSelfService/KratosSelfService.csproj", "KratosSelfService/"]
RUN dotnet restore "KratosSelfService/KratosSelfService.csproj"
COPY . .
WORKDIR "/src/KratosSelfService"
RUN dotnet build "KratosSelfService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KratosSelfService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KratosSelfService.dll", "--urls", "http://0.0.0.0:3000"]

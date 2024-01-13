FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KratosSelfServiceBlazor/KratosSelfServiceBlazor.csproj", "KratosSelfServiceBlazor/"]
RUN dotnet restore "KratosSelfServiceBlazor/KratosSelfServiceBlazor.csproj"
COPY . .
WORKDIR "/src/KratosSelfServiceBlazor"
RUN dotnet build "KratosSelfServiceBlazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KratosSelfServiceBlazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KratosSelfServiceBlazor.dll", "--urls", "http://0.0.0.0:3000"]

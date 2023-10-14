FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KratosAdmin/KratosAdmin.csproj", "KratosAdmin/"]
RUN dotnet restore "KratosAdmin/KratosAdmin.csproj"
COPY . .
WORKDIR "/src/KratosAdmin"
RUN dotnet build "KratosAdmin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KratosAdmin.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KratosAdmin.dll"]

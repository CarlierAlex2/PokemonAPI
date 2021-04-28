FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Pokemons.API.csproj", "./"]
RUN dotnet restore "Pokemons.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Pokemons.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokemons.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokemons.API.dll"]

﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StarWars.Api.csproj", "./"]
RUN dotnet restore "StarWars.Api.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "StarWars.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StarWars.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarWars.Api.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY quorum-data/. quorum-data/
COPY quorum-api/. quorum-api/
COPY ["quorum-api/quorum-api.csproj", "quorum-api/"]


RUN dotnet restore "quorum-api/quorum-api.csproj"

RUN dotnet build "quorum-api/quorum-api.csproj" -c Release -o /app/build

WORKDIR /src/quorum-api

FROM build AS publish
RUN dotnet publish "quorum-api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
EXPOSE 5001
ENTRYPOINT ["dotnet", "quorum-api.dll"]

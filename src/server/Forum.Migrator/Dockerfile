FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["/Forum.Migrator/Forum.Migrator.csproj", "Forum.Migrator/"]
RUN dotnet restore "Forum.Migrator/Forum.Migrator.csproj"
COPY . .
WORKDIR "/src/Forum.Migrator"
RUN dotnet build "Forum.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Forum.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Forum.Migrator.dll"]

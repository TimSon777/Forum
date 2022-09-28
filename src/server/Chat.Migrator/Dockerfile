FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Chat.Migrator/Chat.Migrator.csproj", "Chat.Migrator/"]
COPY ["Chat.SharedKernel/Chat.SharedKernel.csproj", "Chat.SharedKernel/"]
COPY ["Chat.Shared/Chat.Shared.csproj", "Chat.Shared/"]
COPY ["Chat.DAL.Abstractions/Chat.DAL.Abstractions.csproj", "Chat.DAL.Abstractions/"]
COPY ["Chat.Core/Chat.Core.fsproj", "Chat.Core/"]
COPY ["Chat.DAL.Implementations/Chat.DAL.Implementations.csproj", "Chat.DAL.Implementations/"]
RUN dotnet restore "Chat.Migrator/Chat.Migrator.csproj"
COPY . .
WORKDIR "/src/Chat.Migrator"
RUN dotnet build "Chat.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chat.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.Migrator.dll"]

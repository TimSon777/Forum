FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/FileMetadata/FileMetadata.Queue.Listener/FileMetadata.Queue.Listener.csproj", "FileMetadata/FileMetadata.Queue.Listener/"]
COPY ["/FileMetadata/FileMetadata.Queue.Shared/FileMetadata.Queue.Shared.csproj", "FileMetadata/FileMetadata.Queue.Shared/"]
COPY ["/Cache/Cache.csproj", "Cache/"]
COPY ["/Shared/Shared.csproj", "Shared/"]
COPY ["/SharedKernel/SharedKernel.csproj", "SharedKernel/"]
RUN dotnet restore "FileMetadata/FileMetadata.Queue.Listener/FileMetadata.Queue.Listener.csproj"
COPY . .
WORKDIR "/src/FileMetadata/FileMetadata.Queue.Listener"
RUN dotnet build "FileMetadata.Queue.Listener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileMetadata.Queue.Listener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileMetadata.Queue.Listener.dll"]

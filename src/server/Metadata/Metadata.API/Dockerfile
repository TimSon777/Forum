FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/Metadata/Metadata.API/Metadata.API.csproj", "Metadata/Metadata.API/"]
COPY ["/SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["/Shared/Shared.csproj", "Shared/"]
COPY ["/Cache/Cache.csproj", "Cache/"]
COPY ["/Metadata/Metadata.Infrastructure/Metadata.Infrastructure.csproj", "Metadata/Metadata.Infrastructure/"]
COPY ["/Metadata/Metadata.Application/Metadata.Application.csproj", "Metadata/Metadata.Application/"]
COPY ["/Metadata/Metadata.Domain/Metadata.Domain.csproj", "Metadata/Metadata.Domain/"]
COPY ["/FileMetadata/FileMetadata.Queue.Shared/FileMetadata.Queue.Shared.csproj", "FileMetadata/FileMetadata.Queue.Shared/"]
RUN dotnet restore "Metadata/Metadata.API/Metadata.API.csproj"
COPY . .
WORKDIR "/src/Metadata/Metadata.API"
RUN dotnet build "Metadata.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Metadata.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Metadata.API.dll"]

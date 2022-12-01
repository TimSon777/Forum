FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/File/File.API/File.API.csproj", "File/File.API/"]
COPY ["/SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["/Shared/Shared.csproj", "Shared/"]
COPY ["/File/File.Infrastructure/File.Infrastructure.csproj", "File/File.Infrastructure/"]
COPY ["/File/File.Application/File.Application.csproj", "File/File.Application/"]
COPY ["/File/File.Domain/File.Domain.csproj", "File/File.Domain/"]
COPY ["/FileMetadata/FileMetadata.Queue.Shared/FileMetadata.Queue.Shared.csproj", "FileMetadata/FileMetadata.Queue.Shared/"]
RUN dotnet restore "File/File.API/File.API.csproj"
COPY . .
WORKDIR "/src/File/File.API"
RUN dotnet build "File.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "File.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "File.API.dll"]

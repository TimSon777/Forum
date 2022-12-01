FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/Forum/Forum.API/Forum.API.csproj", "Forum/Forum.API/"]
COPY ["/Forum/Forum.Infrastructure/Forum.Infrastructure.csproj", "Forum/Forum.Infrastructure/"]
COPY ["/Forum/Forum.Queue.Shared/Forum.Queue.Shared.csproj", "Forum/Forum.Queue.Shared/"]
COPY ["/Forum/Forum.Application/Forum.Application.csproj", "Forum/Forum.Application/"]
COPY ["/Forum/Forum.Domain/Forum.Domain.csproj", "Forum/Forum.Domain/"]
COPY ["/SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["/Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Forum/Forum.API/Forum.API.csproj"
COPY . .
WORKDIR "/src/Forum/Forum.API"
RUN dotnet build "Forum.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Forum.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Forum.API.dll"]

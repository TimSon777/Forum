FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/Forum/Forum.Queue.Listener/Forum.Queue.Listener.csproj", "Forum/Forum.Queue.Listener/"]
COPY ["/Forum/Forum.Queue.Shared/Forum.Queue.Shared.csproj", "Forum/Forum.Queue.Shared/"]
COPY ["/Shared/Shared.csproj", "Shared/"]
COPY ["/SharedKernel/SharedKernel.csproj", "SharedKernel/"]
RUN dotnet restore "Forum/Forum.Queue.Listener/Forum.Queue.Listener.csproj"
COPY . .
WORKDIR "/src/Forum/Forum.Queue.Listener"
RUN dotnet build "Forum.Queue.Listener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Forum.Queue.Listener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Forum.Queue.Listener.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LiWiMus.Web.MVC/LiWiMus.Web.MVC.csproj", "LiWiMus.Web.MVC/"]
COPY ["LiWiMus.Core/LiWiMus.Core.csproj", "LiWiMus.Core/"]
COPY ["LiWiMus.SharedKernel/LiWiMus.SharedKernel.csproj", "LiWiMus.SharedKernel/"]
COPY ["LiWiMus.Infrastructure/LiWiMus.Infrastructure.csproj", "LiWiMus.Infrastructure/"]
COPY ["LiWiMus.Web.Shared/LiWiMus.Web.Shared.csproj", "LiWiMus.Web.Shared/"]
RUN dotnet restore "LiWiMus.Web.MVC/LiWiMus.Web.MVC.csproj"
COPY . .
WORKDIR "/src/LiWiMus.Web.MVC"
RUN dotnet build "LiWiMus.Web.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LiWiMus.Web.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LiWiMus.Web.MVC.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG project_name
WORKDIR /src

COPY ["/Services/${project_name}/${project_name}.csproj", "Services/${project_name}/"]
COPY ["/SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["/Application/Application.csproj", "Application/"]
COPY ["/Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "Services/${project_name}/${project_name}.csproj"
COPY . .
WORKDIR "/src/Services/${project_name}"
RUN dotnet build "${project_name}.csproj" -c Release -o /app/build

FROM build AS publish
ARG project_name
RUN dotnet publish "${project_name}.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ARG project_name
ENV PROJECT_NAME=${project_name}

ENTRYPOINT ["/bin/sh", "-c", "dotnet ${PROJECT_NAME}.dll"]

#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#ARG project_name
#ENV PROJECT_NAME=${project_name}
#
#WORKDIR /src
#
#COPY ["/Services/${project_name}/${project_name}.csproj", "Services/${project_name}/"]
#COPY ["/SharedKernel/SharedKernel.csproj", "SharedKernel/"]
#COPY ["/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
#COPY ["/Application/Application.csproj", "Application/"]
#COPY ["/Domain/Domain.csproj", "Domain/"]
#RUN dotnet restore "Services/${project_name}/${project_name}.csproj"
#COPY . .
#WORKDIR "/src/Services/${project_name}"
#RUN dotnet build "${project_name}.csproj" -c Debug
#
#ENTRYPOINT ["/bin/sh", "-c", "dotnet run -c Debug --no-launch-profile --no-build --"]
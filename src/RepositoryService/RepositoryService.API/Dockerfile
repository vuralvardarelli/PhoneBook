#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/RepositoryService/RepositoryService.API/RepositoryService.API.csproj", "src/RepositoryService/RepositoryService.API/"]
COPY ["src/RepositoryService/RepositoryService.Infrastructure/RepositoryService.Infrastructure.csproj", "src/RepositoryService/RepositoryService.Infrastructure/"]
COPY ["src/RepositoryService/RepositoryService.Core/RepositoryService.Core.csproj", "src/RepositoryService/RepositoryService.Core/"]
COPY ["src/RepositoryService/RepositoryService.Application/RepositoryService.Application.csproj", "src/RepositoryService/RepositoryService.Application/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
RUN dotnet restore "src/RepositoryService/RepositoryService.API/RepositoryService.API.csproj"
COPY . .
WORKDIR "/src/src/RepositoryService/RepositoryService.API"
RUN dotnet build "RepositoryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RepositoryService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RepositoryService.API.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ReportService/ReportService.API/ReportService.API.csproj", "src/ReportService/ReportService.API/"]
COPY ["src/ReportService/ReportService.Core/ReportService.Core.csproj", "src/ReportService/ReportService.Core/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
COPY ["src/ReportService/ReportService.Infrastructure/ReportService.Infrastructure.csproj", "src/ReportService/ReportService.Infrastructure/"]
RUN dotnet restore "src/ReportService/ReportService.API/ReportService.API.csproj"
COPY . .
WORKDIR "/src/src/ReportService/ReportService.API"
RUN dotnet build "ReportService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.API.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PhoneBook/PhoneBook.API/PhoneBook.API.csproj", "src/PhoneBook/PhoneBook.API/"]
COPY ["src/PhoneBook/PhoneBook.Infrastructure/PhoneBook.Infrastructure.csproj", "src/PhoneBook/PhoneBook.Infrastructure/"]
COPY ["src/PhoneBook/PhoneBook.Core/PhoneBook.Core.csproj", "src/PhoneBook/PhoneBook.Core/"]
RUN dotnet restore "src/PhoneBook/PhoneBook.API/PhoneBook.API.csproj"
COPY . .
WORKDIR "/src/src/PhoneBook/PhoneBook.API"
RUN dotnet build "PhoneBook.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhoneBook.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBook.API.dll"]

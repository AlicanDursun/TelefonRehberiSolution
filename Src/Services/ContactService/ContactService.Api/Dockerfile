#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/Services/ContactService/ContactService.Api/ContactService.Api.csproj", "Src/Services/ContactService/ContactService.Api/"]
COPY ["Src/BuildingBlocks/EventBus/EventBus.Base/EventBus.Base.csproj", "Src/BuildingBlocks/EventBus/EventBus.Base/"]
COPY ["Src/BuildingBlocks/EventBus/EventBus.Factory/EventBus.Factory.csproj", "Src/BuildingBlocks/EventBus/EventBus.Factory/"]
COPY ["Src/BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "Src/BuildingBlocks/EventBus/EventBus.RabbitMQ/"]
RUN dotnet restore "Src/Services/ContactService/ContactService.Api/ContactService.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/ContactService/ContactService.Api"
RUN dotnet build "ContactService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactService.Api.dll"]
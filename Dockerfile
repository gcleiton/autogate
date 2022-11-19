FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8000;http://+:80;

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/IFCE.AutoGate.API/IFCE.AutoGate.API.csproj", "IFCE.AutoGate.API/"]
COPY ["src/IFCE.AutoGate.Core/IFCE.AutoGate.Core.csproj", "IFCE.AutoGate.Core/"]
COPY ["src/IFCE.AutoGate.Domain/IFCE.AutoGate.Domain.csproj", "IFCE.AutoGate.Domain/"]
COPY ["src/IFCE.AutoGate.Infrastructure/IFCE.AutoGate.Infrastructure.csproj", "IFCE.AutoGate.Infrastructure/"]
COPY ["src/IFCE.AutoGate.Application/IFCE.AutoGate.Application.csproj", "IFCE.AutoGate.Application/"]

RUN dotnet restore "IFCE.AutoGate.API/IFCE.AutoGate.API.csproj"
COPY . .
WORKDIR "src/IFCE.AutoGate.API"
RUN dotnet build "IFCE.AutoGate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IFCE.AutoGate.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IFCE.AutoGate.API.dll"]

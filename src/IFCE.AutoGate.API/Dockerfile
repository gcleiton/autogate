FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app    

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/IFCE.AutoGate.API/IFCE.AutoGate.API.csproj", "IFCE.AutoGate.API/"]
RUN dotnet restore "src/IFCE.AutoGate.API/IFCE.AutoGate.API.csproj"
COPY . .
WORKDIR "/src/IFCE.AutoGate.API"
RUN dotnet build "IFCE.AutoGate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IFCE.AutoGate.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "IFCE.AutoGate.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT IFCE.AutoGate.API.dll

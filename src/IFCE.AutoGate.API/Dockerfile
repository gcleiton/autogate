FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/src/IFCE.AutoGate.API/IFCE.AutoGate.API.csproj", "IFCE.AutoGate.API/"]
RUN dotnet restore "/src/IFCE.AutoGate.API/IFCE.AutoGate.API.csproj"
COPY /src .
WORKDIR "/src/IFCE.AutoGate.API"
RUN dotnet build "IFCE.AutoGate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IFCE.AutoGate.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IFCE.AutoGate.API.dll"]
#CMD ASPNETCORE_URLS="http://*:$PORT" dotnet IFCE.AutoGate.API.dll
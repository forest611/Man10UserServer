FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Man10UserServer.csproj", "Man10UserServer/"]
RUN dotnet restore "Man10UserServer/Man10UserServer.csproj"
COPY . Man10UserServer
WORKDIR "/src/Man10UserServer"
RUN dotnet build "Man10UserServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Man10UserServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Man10UserServer.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TeamHostApp.WEB/TeamHostApp.WEB.csproj", "TeamHostApp.WEB/"]
RUN dotnet restore "TeamHostApp.WEB/TeamHostApp.WEB.csproj"
COPY . .
WORKDIR "/src/TeamHostApp.WEB"
RUN dotnet build "TeamHostApp.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamHostApp.WEB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamHostApp.WEB.dll"]

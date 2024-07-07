FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["clodlog-backend/clodlog-backend.csproj", "clodlog-backend/"]
RUN dotnet restore "clodlog-backend/clodlog-backend.csproj"
COPY . .
WORKDIR "/src/clodlog-backend"
RUN dotnet build "clodlog-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "clodlog-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "clodlog-backend.dll"]

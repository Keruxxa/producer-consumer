FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src
COPY ["src/Producer/Producer.csproj", "Producer/"]
COPY ["src/Shared.Contracts/Shared.Contracts.csproj", "Shared.Contracts/"]
RUN dotnet restore "Producer/Producer.csproj"

COPY /src .

RUN dotnet build "Producer/Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Producer/Producer.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Producer.dll"]

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src
COPY ["src/Consumer/Consumer.csproj", "Consumer/"]
COPY ["src/Shared.Contracts/Shared.Contracts.csproj", "Shared.Contracts/"]
RUN dotnet restore "Consumer/Consumer.csproj"

COPY /src .
RUN dotnet build "Consumer/Consumer.csproj" -c Release -o /app/build

from build as publish
RUN dotnet publish "Consumer/Consumer.csproj" \
	-c Release \
	-o /app/build \
	--no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "Consumer.dll"]
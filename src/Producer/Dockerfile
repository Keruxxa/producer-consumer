FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /src
COPY ["Producer.csproj", "./"]
RUN dotnet restore "./Producer.csproj"

COPY . .

WORKDIR "/src/Producer"
RUN dotnet publish "Producer.csproj" -c Release -o /app/publish

EXPOSE 8080

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Barrier.Core.dll"]

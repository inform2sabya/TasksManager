FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
WORKDIR /src
COPY TasksApi.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish "TasksApi.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "TasksApi.dll"]

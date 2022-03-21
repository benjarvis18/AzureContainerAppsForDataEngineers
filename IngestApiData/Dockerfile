#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IngestApiData/IngestApiData.csproj", "IngestApiData/"]
RUN dotnet restore "IngestApiData/IngestApiData.csproj"
COPY . .
WORKDIR "/src/IngestApiData"
RUN dotnet build "IngestApiData.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IngestApiData.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IngestApiData.dll"]
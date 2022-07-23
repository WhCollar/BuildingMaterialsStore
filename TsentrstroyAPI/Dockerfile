FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TsentrstroyAPI/TsentrstroyAPI.csproj", "TsentrstroyAPI/"]
COPY ["TsentrstroyAPI.Data/TsentrstroyAPI.Data.csproj", "TsentrstroyAPI.Data/"]
RUN dotnet restore "TsentrstroyAPI/TsentrstroyAPI.csproj"
COPY . .
WORKDIR "/src/TsentrstroyAPI"
RUN dotnet build "TsentrstroyAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TsentrstroyAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TsentrstroyAPI.dll"]

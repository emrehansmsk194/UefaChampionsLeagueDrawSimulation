FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY UclDrawAPI/UclDrawAPI.csproj UclDrawAPI/
RUN dotnet restore UclDrawAPI/UclDrawAPI.csproj
COPY UclDrawAPI/ UclDrawAPI/
WORKDIR /src/UclDrawAPI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "UclDrawAPI.dll"]

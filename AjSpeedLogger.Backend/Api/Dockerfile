# BUILD
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# COPY FILES, RESTORE DEPENDENCIES
COPY . .
RUN dotnet restore

# PUBLISH APP IN RELESE MODE
RUN dotnet publish -c Release -o /app/publish

# RUN
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# EXPOSE PORT
EXPOSE 8000
ENV ASPNETCORE_URLS=http://+:8000

# START APPLICATION
ENTRYPOINT ["dotnet", "Api.dll"]
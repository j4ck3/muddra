FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY muddraWebApp/muddraWebApp.csproj ./

RUN dotnet restore

COPY muddraWebApp/ ./

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080 
EXPOSE 8081

ARG DEFAULT_CONNECTION
ARG HOST
ARG SMTP_PORT
ARG SMTP_USERNAME
ARG SMTP_KEY
ARG RECIVER_1
ARG RECIVER_2

ENTRYPOINT ["dotnet", "muddraWebApp.dll"]

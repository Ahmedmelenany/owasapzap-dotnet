FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY SimpleApi.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN groupadd --system --gid 1001 appuser \
 && useradd --system --uid 1001 --gid appuser --shell /usr/sbin/nologin appuser

COPY --from=build --chown=appuser:appuser /app ./

ENV ASPNETCORE_URLS=http://+:5132 \
    ASPNETCORE_ENVIRONMENT=Development \
    DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 5132

USER appuser

ENTRYPOINT ["dotnet", "SimpleApi.dll"]

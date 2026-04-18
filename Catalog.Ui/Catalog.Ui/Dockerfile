# Dockerfile for Catalog.Ui (Blazor hosting project)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Ajuste: os projetos do UI estão dentro da pasta `Catalog.Ui/` no repositório,
# então os caminhos de origem precisam incluir esse nível adicional.
COPY ["Catalog.Ui/Catalog.Ui/Catalog.Ui.csproj", "Catalog.Ui/"]
COPY ["Catalog.Ui/Catalog.Ui.Client/Catalog.Ui.Client.csproj", "Catalog.Ui.Client/"]
COPY ["Catalog.Ui/Catalog.UI.Shared/Catalog.UI.Shared.csproj", "Catalog.UI.Shared/"]
# copy other projects if needed
COPY ["Catalog.CrossCutting/Catalog.CrossCutting.csproj", "Catalog.CrossCutting/"]
COPY ["Catalog.Application/Catalog.Application.csproj", "Catalog.Application/"]
COPY ["Catalog.Data/Catalog.Data.csproj", "Catalog.Data/"]
COPY ["Catalog.Domain/Catalog.Domain.csproj", "Catalog.Domain/"]

RUN dotnet restore "Catalog.Ui/Catalog.Ui.csproj"
COPY . .
WORKDIR "/src/Catalog.Ui"
RUN dotnet build "Catalog.Ui.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.Ui.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Ui.dll"]

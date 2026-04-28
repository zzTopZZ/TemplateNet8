FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Catalog.Ui/Catalog.Ui/Catalog.Ui.csproj", "Catalog.Ui/"]
COPY ["Catalog.Ui/Catalog.Ui.Client/Catalog.Ui.Client.csproj", "Catalog.Ui.Client/"]
COPY ["Catalog.Ui/Catalog.UI.Shared/Catalog.UI.Shared.csproj", "Catalog.UI.Shared/"]
COPY ["Catalog.CrossCutting/Catalog.CrossCutting.csproj", "Catalog.CrossCutting/"]
COPY ["Catalog.Application/Catalog.Application.csproj", "Catalog.Application/"]
COPY ["Catalog.Data/Catalog.Data.csproj", "Catalog.Data/"]
COPY ["Catalog.Domain/Catalog.Domain.csproj", "Catalog.Domain/"]

RUN dotnet restore "Catalog.Ui/Catalog.Ui.csproj"

COPY . .
WORKDIR "/src/Catalog.Ui"

RUN dotnet publish "Catalog.Ui.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Ui.dll"]
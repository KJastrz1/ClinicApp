FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClinicApp.App/ClinicApp.App.csproj", "Backend/ClinicApp.App/"]
COPY ["ClinicApp.Application/ClinicApp.Application.csproj", "Backend/ClinicApp.Application/"]
COPY ["ClinicApp.Domain/ClinicApp.Domain.csproj", "Backend/ClinicApp.Domain/"]
COPY ["ClinicApp.Infrastructure/ClinicApp.Infrastructure.csproj", "Backend/ClinicApp.Infrastructure/"]
COPY ["ClinicApp.Persistence/ClinicApp.Persistence.csproj", "Backend/ClinicApp.Persistence/"]
COPY ["ClinicApp.Presentation/ClinicApp.Presentation.csproj", "Backend/ClinicApp.Presentation/"]
RUN dotnet restore "Backend/ClinicApp.App/ClinicApp.App.csproj"
COPY . .
WORKDIR "/src/Backend/ClinicApp.App"
RUN dotnet build "ClinicApp.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ClinicApp.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClinicApp.App.dll"]

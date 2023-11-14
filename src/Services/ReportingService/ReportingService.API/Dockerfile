#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/ReportingService/ReportingService.API/ReportingService.API.csproj", "src/Services/ReportingService/ReportingService.API/"]
COPY ["src/Services/ReportingService/ReportingService.Infrastructure/ReportingService.Infrastructure.csproj", "src/Services/ReportingService/ReportingService.Infrastructure/"]
COPY ["src/Services/ReportingService/ReportingService.Application/ReportingService.Application.csproj", "src/Services/ReportingService/ReportingService.Application/"]
COPY ["src/Services/ReportingService/ReportingService.Domain/ReportingService.Domain.csproj", "src/Services/ReportingService/ReportingService.Domain/"]
COPY ["src/Common/Event/Event.csproj", "src/Common/Event/"]
COPY ["src/Common/Shared/Shared.csproj", "src/Common/Shared/"]
RUN dotnet restore "src/Services/ReportingService/ReportingService.API/ReportingService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/ReportingService/ReportingService.API"
RUN dotnet build "ReportingService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportingService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportingService.API.dll"]
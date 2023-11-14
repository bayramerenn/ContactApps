#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/ContactDirectoryService/ContactDirectoryService.API/ContactDirectoryService.API.csproj", "src/Services/ContactDirectoryService/ContactDirectoryService.API/"]
COPY ["src/Services/ContactDirectoryService/ContactDirectoryService.Infrastructure/ContactDirectoryService.Infrastructure.csproj", "src/Services/ContactDirectoryService/ContactDirectoryService.Infrastructure/"]
COPY ["src/Services/ContactDirectoryService/ContactDirectoryService.Application/ContactDirectoryService.Application.csproj", "src/Services/ContactDirectoryService/ContactDirectoryService.Application/"]
COPY ["src/Services/ContactDirectoryService/ContactDirectoryService.Domain/ContactDirectoryService.Domain.csproj", "src/Services/ContactDirectoryService/ContactDirectoryService.Domain/"]
COPY ["src/Common/Event/Event.csproj", "src/Common/Event/"]
COPY ["src/Common/Shared/Shared.csproj", "src/Common/Shared/"]
RUN dotnet restore "src/Services/ContactDirectoryService/ContactDirectoryService.API/ContactDirectoryService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/ContactDirectoryService/ContactDirectoryService.API"
RUN dotnet build "ContactDirectoryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactDirectoryService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactDirectoryService.API.dll"]
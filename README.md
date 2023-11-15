# .NET Core Microservices ContactApps and Docker containers

## Getting Started

Make sure you have [installed](https://docs.docker.com/docker-for-windows/install/) and [configured](https://github.com/dotnet-architecture/eShopOnContainers/wiki/Windows-setup#configure-docker) docker in your environment. After that, you can run the below commands from the **/src/** directory and get started with the `ContactApps` immediately.

```powershell
docker-compose build
docker-compose up
```

You should be able to browse different components of the application by using the below URLs :

```
ApiGateway : http://host.docker.internal:7000/
ContactDirectoryService.API :  http://host.docker.internal:7001/
ReportingService.API :  http://host.docker.internal:7002/
```

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
### Architecture overview
This reference application provides cross-platform support on both the server and client sides through .NET 7 services capable of running on Linux or Windows. The architecture recommends a microservices-oriented design, including multiple independent microservices, each with its own database. These microservices utilize HTTP as the communication protocol between client applications and microservices. Additionally, the architecture supports the propagation of data updates across multiple services through integration events and event publishing, facilitated by a lightweight messaging tool (such as RabbitMQ).






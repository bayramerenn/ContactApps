using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ReportingService.Application.Consumers;
using ReportingService.Infrastructure.Persistence.Context;
using System.Data.Common;

namespace ReportingService.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly DbConnection _connection;

        public CustomWebApplicationFactory(DbConnection connection)
        {
            _connection = connection;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services
                    .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                    .AddDbContext<ApplicationDbContext>((sp, options) =>
                    {
                        options.UseNpgsql(_connection);
                    });

                services.AddMassTransitTestHarness(x =>
                {
                    x.AddConsumer<FailContractReportConsumer>();
                    x.AddConsumer<SuccessContractReportConsumer>();
                });
            });
        }
    }
}
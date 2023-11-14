using Microsoft.EntityFrameworkCore;
using ReportingService.Application;
using ReportingService.Infrastructure;
using ReportingService.Infrastructure.Persistence.Context;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddConfigureApplication(builder.Configuration)
    .AddConfigureMassTransit(builder.Configuration)
    .AddConfigureInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateAsyncScope();
var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

try
{
    await applicationDbContext.Database.MigrateAsync();
}
catch
{
    await applicationDbContext.Database.EnsureDeletedAsync();
    await applicationDbContext.Database.MigrateAsync();
}

app.Run();

public partial class Program
{ }
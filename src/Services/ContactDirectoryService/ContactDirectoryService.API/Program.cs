using ContactDirectoryService.Application;
using ContactDirectoryService.Infrastructure;
using ContactDirectoryService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NewInn.Shared.Filters;
using Shared.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<DefaultResponseAttribute>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

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
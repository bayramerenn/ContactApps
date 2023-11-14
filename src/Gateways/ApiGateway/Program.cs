using ApiGateway.Filters;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddSwaggerForOcelot(builder.Configuration, swagger =>
{
    swagger.GenerateDocsDocsForGatewayItSelf(opt =>
    {
        opt.GatewayDocsTitle = "ApiGateway";
        opt.GatewayDocsOpenApiInfo = new()
        {
            Title = "ApiGateway",
            Version = "v1",
        };

        opt.DocumentFilter<SwaggerDocumentFilter>();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAnyOrigin");

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
}, uiOpt =>
{
    uiOpt.DefaultModelsExpandDepth(-1);
});
app.UseOcelot()
   .Wait();

app.Run();
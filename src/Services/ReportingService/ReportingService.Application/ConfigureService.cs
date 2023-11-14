using Ardalis.GuardClauses;
using Event;
using Event.Services;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviours.Validator;
using Shared.CacheService;
using System.Reflection;

namespace ReportingService.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));

            services.AddSingleton<IRedisCache>(sp =>
            {
                var host = configuration.GetConnectionString("Redis");
                Guard.Against.NullOrEmpty(host);
                var redis = new RedisCache(host);
                redis.Connect();
                return redis;
            });

            return services;
        }

        public static IServiceCollection AddConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IQueueService, QueueService>();

            var rabbitMqSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseMessageRetry(r => r.Immediate(5));
                    cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));

                    cfg.Host(rabbitMqSettings!.Url, rabbitMqSettings.Port, "/", host =>
                    {
                        host.Username(rabbitMqSettings.Username);
                        host.Password(rabbitMqSettings.Password);
                    });
                });
            });

            return services;
        }
    }
}
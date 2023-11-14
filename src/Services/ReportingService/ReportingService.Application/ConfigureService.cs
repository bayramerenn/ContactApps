using Ardalis.GuardClauses;
using FluentValidation;
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
    }
}
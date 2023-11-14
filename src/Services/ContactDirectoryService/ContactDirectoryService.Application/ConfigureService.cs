using Ardalis.GuardClauses;
using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Common.Behaviours.Validator;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.CacheService;
using System.Reflection;

namespace ContactDirectoryService.Application
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
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
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
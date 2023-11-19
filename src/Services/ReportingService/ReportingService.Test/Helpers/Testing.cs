using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReportingService.Infrastructure.Persistence.Context;
using ReportingService.Test.DbBehaviors;
using Shared.CacheService;

namespace ReportingService.Test
{
    [SetUpFixture]
    public partial class Testing
    {
        private static ITestDatabase _database;
        private static CustomWebApplicationFactory _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            _database = await TestDatabaseFactory.CreateAsync();

            _factory = new CustomWebApplicationFactory(_database.GetConnection());

            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task SendAsync(IBaseRequest request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            await mediator.Send(request);
        }

        public static async Task CacheSaveAsync<T>(T data, string key)
        {
            using var scope = _scopeFactory.CreateScope();

            var cache = scope.ServiceProvider.GetRequiredService<IRedisCache>();

            await cache.SaveAsync(key, data, TimeSpan.FromSeconds(1000));
        }

        public static async Task ResetState()
        {
            await _database.ResetAsync();
        }

        public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            await _database.DisposeAsync();
            await _factory.DisposeAsync();
        }
    }
}
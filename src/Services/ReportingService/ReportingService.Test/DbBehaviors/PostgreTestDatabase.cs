using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ReportingService.Infrastructure.Persistence.Context;
using Respawn;
using System.Data.Common;

namespace ReportingService.Test.DbBehaviors
{
    public class PostgreTestDatabase : ITestDatabase
    {
        private readonly string _connectionString;
        private NpgsqlConnection _connection = null!;
        private Respawner _respawner = null!;

        public PostgreTestDatabase()
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddEnvironmentVariables()
           .Build();

            var connectionString = configuration.GetConnectionString("PostgreConnection");

            Guard.Against.Null(connectionString);

            _connectionString = connectionString;
        }

        public async Task InitialiseAsync()
        {
            _connection = new NpgsqlConnection(_connectionString);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(_connectionString)
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.Migrate();

            await _connection.OpenAsync();

            _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
            });
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }

        public async Task ResetAsync()
        {
            await _respawner.ResetAsync(_connection);
        }

        public async Task DisposeAsync()
        {
            await _connection.DisposeAsync();
        }
    }
}
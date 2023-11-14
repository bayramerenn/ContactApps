using System.Data.Common;

namespace ReportingService.Test
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();

        DbConnection GetConnection();

        Task ResetAsync();

        Task DisposeAsync();
    }
}
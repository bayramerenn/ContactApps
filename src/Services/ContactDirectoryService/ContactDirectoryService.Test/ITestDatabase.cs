using System.Data.Common;

namespace ContactDirectoryService.Test
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();

        DbConnection GetConnection();

        Task ResetAsync();

        Task DisposeAsync();
    }
}
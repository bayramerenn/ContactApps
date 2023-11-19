using System.Data.Common;

namespace ContactDirectoryService.Test.Features
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();

        DbConnection GetConnection();

        Task ResetAsync();

        Task DisposeAsync();
    }
}
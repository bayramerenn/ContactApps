using NUnit.Framework;
using static ContactDirectoryService.Test.Testing;

namespace ContactDirectoryService.Test
{
    [TestFixture]
    public abstract class BaseTestFixture
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
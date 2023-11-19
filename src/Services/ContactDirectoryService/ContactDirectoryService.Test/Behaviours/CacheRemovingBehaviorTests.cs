using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Features.ContactInformations.Command;
using Moq;
using Shared.CacheService;
using Shared.Enums;

namespace ContactDirectoryService.Test.Behaviours
{
    public class CacheRemovingBehaviorTests : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Remove_Cache()
        {
            var cacheMock = new Mock<IRedisCache>();

            var cachingBehavior = new CacheRemovingBehavior<CreateContactInformationCommand, Guid>(cacheMock.Object);

            var query = new CreateContactInformationCommand(Guid.NewGuid(), ContactType.Phone, "1234");

            cacheMock.Setup(x => x.RemovePatternAsync(It.IsAny<string>(), 0))
                .Returns(Task.CompletedTask);

            await cachingBehavior.Handle(query,
                () => Task.FromResult(Guid.NewGuid()),
                CancellationToken.None);

            cacheMock.Verify(x => x.RemovePatternAsync(query.CacheKey, 0), Times.Once);
        }
    }
}
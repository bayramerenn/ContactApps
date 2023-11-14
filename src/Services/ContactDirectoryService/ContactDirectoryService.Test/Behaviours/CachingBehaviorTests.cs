using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Features.ContactInformations.Queries;
using Microsoft.Extensions.Options;
using Moq;
using Shared.CacheService;

namespace ContactDirectoryService.Test.Behaviours
{
    public class CachingBehaviorTests : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Return_Cached_Response()
        {
            var cacheMock = new Mock<IRedisCache>();
            var cacheSettings = new CacheSettings { SlidingExpiration = 300 }; // Set a sliding expiration time for testing

            var cachingBehavior = new CachingBehavior<GetLocationReportQuery, IEnumerable<GetLocationReportResponse>>(cacheMock.Object, Options.Create(cacheSettings));
            var query = new GetLocationReportQuery(Guid.NewGuid());

            var cachedResponse = new List<GetLocationReportResponse> { new GetLocationReportResponse() };

            cacheMock.Setup(x => x.GetAsync<IEnumerable<GetLocationReportResponse>>(It.IsAny<string>(), 0))
                .ReturnsAsync(cachedResponse);

            var result = await cachingBehavior.Handle(query,
                  () => Task.FromResult<IEnumerable<GetLocationReportResponse>>(cachedResponse.ToList()),
                  CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<GetLocationReportResponse>>();

            cacheMock.Verify(x => x.GetAsync<IEnumerable<GetLocationReportResponse>>(query.CacheKey, 0), Times.Once);
        }
    }
}
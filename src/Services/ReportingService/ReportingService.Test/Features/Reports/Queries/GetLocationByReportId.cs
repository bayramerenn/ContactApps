using Ardalis.GuardClauses;
using ReportingService.Application.Features.Reports.Commands;
using ReportingService.Application.Features.Reports.Queries;
using Shared.Constants;

namespace ReportingService.Test.Application.Features.Reports.Queries
{
    public class GetLocationByReportId : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Return_LocationReports()
        {
            var data = new List<GetLocationReportResponse>
            {
                new GetLocationReportResponse
                {
                    ContractCount = 1,
                    Location = "Istanbul",
                    PhoneCount = 2
                },
                new GetLocationReportResponse
                {
                    ContractCount = 13,
                    Location = "Ankara",
                    PhoneCount = 3
                }
            };

            var id = await SendAsync(new CreateLocationReportCommand(Guid.Empty));

            await CacheSaveAsync(data, CacheKeyConstant.GetReportKey(id));

            var result = await SendAsync(new GetLocationByReportIdQuery(id));

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<GetLocationReportResponse>>();
            result.Should().HaveCount(2);
        }

        [Test]
        public async Task Handle_Should_Throw_NotFoundException_When_LocationReport_NotFound()
        {
            await FluentActions.Invoking(() => SendAsync(new GetLocationByReportIdQuery(Guid.NewGuid())))
                .Should().ThrowAsync<NotFoundException>();
        }
    }
}
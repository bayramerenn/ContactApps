using ReportingService.Application.Features.Reports.Commands;
using ReportingService.Application.Features.Reports.Queries;

namespace ReportingService.Test.Application.Features.Reports.Queries
{
    public class GetAllReportTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Return_Reports()
        {
            await SendAsync(new CreateLocationReportCommand(Guid.Empty));
            await SendAsync(new CreateLocationReportCommand(Guid.Empty));
            await SendAsync(new CreateLocationReportCommand(Guid.Empty));

            var result = await SendAsync(new GetAllReportQuery());

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<GetAllReportResponse>>();
            result.Should().HaveCount(3);
        }
    }
}
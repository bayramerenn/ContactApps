using Ardalis.GuardClauses;
using ReportingService.Application.Features.Reports.Commands;

namespace ReportingService.Test.Application.Features.Reports.Commands
{
    public class CreateLocationReportTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Create_Report()
        {
            var result = await SendAsync(new CreateLocationReportCommand(Guid.Empty));

            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task Handle_Should_Update_Report()
        {
            var id = await SendAsync(new CreateLocationReportCommand(Guid.Empty));

            var update = await SendAsync(new CreateLocationReportCommand(id));

            update.Should().NotBeEmpty();

            update.Should().Be(id);
        }

        [Test]
        public async Task Handle_Should_Throw_NotFoundException_When_Report_NotFound()
        {
            await FluentActions.Invoking(() => SendAsync(new CreateLocationReportCommand(Guid.NewGuid())))
                .Should().ThrowAsync<NotFoundException>();
        }
    }
}
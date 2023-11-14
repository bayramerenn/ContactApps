using ContactDirectoryService.Application.Features.ContactInformations.Command;
using ContactDirectoryService.Application.Features.ContactInformations.Queries;
using ContactDirectoryService.Application.Features.Contacts.Commands;
using ContactDirectoryService.Domain.Enums;

namespace ContactDirectoryService.Test.Features.ContactInformations.Queries
{
    public class GetLocationReportTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Return_LocationReport()
        {
            var contactId = await SendAsync(new CreateContactCommand("Bayram", "EREN", "ABC Company"));

            await SendAsync(new CreateContactInformationCommand(contactId, ContactType.Phone, "123456789"));
            await SendAsync(new CreateContactInformationCommand(contactId, ContactType.Location, "Kocaeli"));

            var query = new GetLocationReportQuery(Guid.NewGuid());

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<GetLocationReportResponse>>();
        }
    }
}
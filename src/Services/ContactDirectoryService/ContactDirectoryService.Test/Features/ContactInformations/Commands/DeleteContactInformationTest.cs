using Ardalis.GuardClauses;
using ContactDirectoryService.Application.Features.ContactInformations.Command;
using ContactDirectoryService.Application.Features.Contacts.Commands;
using ContactDirectoryService.Domain.Entities;
using ContactDirectoryService.Domain.Enums;
using MediatR;

namespace ContactDirectoryService.Test.Features.ContactInformations.Commands
{
    public class DeleteContactInformationTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Delete_ContactInformation()
        {
            var contactId = await SendAsync(new CreateContactCommand("Bayram", "EREN", "ABC Company"));

            var command = new CreateContactInformationCommand(contactId, ContactType.Phone, "123456789");

            var contactInformationId = await SendAsync(command);

            var result = await SendAsync(new DeleteContactInformationCommand(contactInformationId));

            result.Should().Be(Unit.Value);

            var deletedContactInformation = await FindAsync<ContactInformation>(contactInformationId);

            deletedContactInformation.Should().BeNull();
        }

        [Test]
        public async Task Handle_Should_Throw_NotFoundException_When_ContactInformation_NotFound()
        {
            var command = new DeleteContactInformationCommand(Guid.NewGuid());

            await FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<NotFoundException>();
        }
    }
}
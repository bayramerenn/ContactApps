using Ardalis.GuardClauses;
using ContactDirectoryService.Application.Features.ContactInformations.Command;
using ContactDirectoryService.Application.Features.Contacts.Commands;
using Shared.Enums;

namespace ContactDirectoryService.Test.Features.ContactInformations.Commands
{
    public class CreateContactInformationTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Create_ContactInformation()
        {
            var contactId = await SendAsync(new CreateContactCommand("Bayram", "EREN", "ABC Company"));

            var command = new CreateContactInformationCommand(contactId, ContactType.Phone, "123456789");

            var result = await SendAsync(command);

            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task Handle_Should_Throw_NotFoundException_When_Contact_NotFound()
        {
            var command = new CreateContactInformationCommand(Guid.NewGuid(), ContactType.Phone, "123456789");

            await FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task Handle_Should_Throw_DuplicateException_When_DuplicateEntity_Exists()
        {
            var contactId = await SendAsync(new CreateContactCommand("Bayram", "EREN", "ABC Company"));

            var command = new CreateContactInformationCommand(contactId, ContactType.Phone, "123456789");

            await SendAsync(command);

            await FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<DuplicateException>();
        }

        [Test]
        public async Task Validate_Should_Fail_When_Content_Is_Empty()
        {
            var command = new CreateContactInformationCommand(Guid.NewGuid(), ContactType.Phone, "");

            await FluentActions.Invoking(() =>
             SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Validate_Should_Fail_When_Invalid_ContactType()
        {
            var command = new CreateContactInformationCommand(Guid.NewGuid(), (ContactType)100, "123456789");

            await FluentActions.Invoking(() =>
                        SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}
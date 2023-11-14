using Ardalis.GuardClauses;
using ContactDirectoryService.Application.Features.Contacts.Commands;
using ContactDirectoryService.Domain.Entities;

namespace ContactDirectoryService.Test.Features.Contacts.Commands
{
    public class UpdateContactTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Update_Contact()
        {
            var id = await SendAsync(new CreateContactCommand("Bayram", "EREN", "ABC Company"));

            var command = new UpdateContactCommand(id, "New First", "New Last", "New Company");

            await SendAsync(command);

            var result = await FindAsync<Contact>(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.FirstName.Should().Be(command.FirstName);
            result.LastName.Should().Be(command.LastName);
            result.Company.Should().Be(command.Company);
        }

        [Test]
        public async Task Should_Have_Error_When_Any_Field_Is_Empty()
        {
            var command = new UpdateContactCommand(Guid.Empty, "", "", "");

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Handle_Should_Throw_Exception_When_Contact_NotFound()
        {
            var id = Guid.NewGuid();
            await FluentActions.Invoking(() =>
                SendAsync(new DeleteContactCommand(id))).Should().ThrowAsync<NotFoundException>();
        }
    }
}
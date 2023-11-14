using Ardalis.GuardClauses;
using ContactDirectoryService.Application.Features.Contacts.Commands;
using ContactDirectoryService.Domain.Entities;
using MediatR;

namespace ContactDirectoryService.Test.Features.Contacts.Commands
{
    public class DeleteContactTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Delete_Contact()
        {
            var createContactCommand = new CreateContactCommand("Bayram", "EREN", "ABC Company");

            var id = await SendAsync(createContactCommand);

            var result = await SendAsync(new DeleteContactCommand(id));

            result.Should().Be(Unit.Value);

            var contact = await FindAsync<Contact>(id);

            contact.Should().BeNull();
        }

        [Test]
        public async Task Handle_Should_Throw_Exception_When_Id_Is_Null_Or_Empty()
        {
            await FluentActions.Invoking(() =>
                SendAsync(new DeleteContactCommand(Guid.Empty))).Should().ThrowAsync<ArgumentException>();
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
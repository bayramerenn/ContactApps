using Ardalis.GuardClauses;
using ContactDirectoryService.Application.Features.Contacts.Commands;
using ContactDirectoryService.Application.Features.Contacts.Queries;

namespace ContactDirectoryService.Test.Features.Queries
{
    public class GetContactDetailTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Throw_Exception_When_Id_Is_Null_Or_Empty()
        {
            await FluentActions.Invoking(() =>
                  SendAsync(new GetContactDetailQuery(Guid.Empty))).Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public async Task Handle_Should_Return_ContactDetail_When_Contact_Exists()
        {
            var id = await SendAsync(new CreateContactCommand("Bayram", "EREN", "ABC Company"));

            var result = await SendAsync(new GetContactDetailQuery(id));

            result.Should().NotBeNull();
        }

        [Test]
        public async Task Handle_Should_Throw_Exception_When_Contact_NotFound()
        {
            await FluentActions.Invoking(() =>
               SendAsync(new DeleteContactCommand(Guid.NewGuid()))).Should().ThrowAsync<NotFoundException>();
        }
    }
}
using ContactDirectoryService.Application.Features.Contacts.Commands;

namespace ContactDirectoryService.Test.Features.Contacts.Commands
{
    public class CreateContactTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Create_Contact()
        {
            var createContactCommand = new CreateContactCommand("Bayram", "EREN", "ABC Company");

            var result = await SendAsync(createContactCommand);

            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task Should_Have_Error_When_Any_Field_Is_Empty()
        {
            var command = new CreateContactCommand("", "", "");

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}
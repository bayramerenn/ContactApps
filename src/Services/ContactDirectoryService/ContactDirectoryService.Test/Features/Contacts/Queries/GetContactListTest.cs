using ContactDirectoryService.Application.Features.Contacts.Queries;
using ContactDirectoryService.Test.BaseTestModels;

namespace ContactDirectoryService.Test.Features.Queries
{
    public class GetContactListTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_Should_Return_ContactList()
        {
            var query = new GetContactListQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<GetContactListResponse>>();
        }
    }
}
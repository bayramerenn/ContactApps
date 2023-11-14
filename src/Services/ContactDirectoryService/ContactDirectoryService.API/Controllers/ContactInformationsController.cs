using ContactDirectoryService.Application.Features.ContactInformations.Command;
using ContactDirectoryService.Application.Features.Contacts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ContactDirectoryService.API.Controllers
{
    public class ContactInformationsController : ApiControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(new GetContactDetailQuery(id), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactInformationCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(command, cancellationToken));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await Sender.Send(new DeleteContactInformationCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
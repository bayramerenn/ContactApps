using ContactDirectoryService.Application.Features.Contacts.Commands;
using ContactDirectoryService.Application.Features.Contacts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ContactDirectoryService.API.Controllers
{
    public class ContactsController : ApiControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(new GetContactDetailQuery(id), cancellationToken));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(new GetContactListQuery(), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(command, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateContactCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(command, cancellationToken));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await Sender.Send(new DeleteContactCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
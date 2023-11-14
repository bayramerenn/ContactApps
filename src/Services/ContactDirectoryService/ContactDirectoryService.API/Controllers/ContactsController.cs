using ContactDirectoryService.Application.Features.Contract.Commands.CreateContact;
using ContactDirectoryService.Application.Features.Contract.Commands.DeleteContact;
using ContactDirectoryService.Application.Features.Contract.Commands.UpdateContact;
using Microsoft.AspNetCore.Mvc;

namespace ContactDirectoryService.API.Controllers
{
    public class ContactsController : ApiControllerBase
    {
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
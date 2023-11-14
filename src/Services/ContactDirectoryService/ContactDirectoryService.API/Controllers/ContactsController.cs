using ContactDirectoryService.Application.Features.Contract.Commands.CreateContact;
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
    }
}
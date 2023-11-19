using ApiGateway.Extensions;
using ApiGateway.Models.Contacts;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactApiClient _contactApiClient;

        public ContactsController(IContactApiClient contactApiClient)
        {
            _contactApiClient = contactApiClient;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _contactApiClient.GetContactDetailAsync(id, cancellationToken);

            return result.ActionResult();
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            var result = await _contactApiClient.GetContactsAsync(cancellationToken);

            return result.ActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateRequest request, CancellationToken cancellationToken)
        {
            var result = await _contactApiClient.CreateAsync(request, cancellationToken);

            return result.ActionResult();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ContactUpdateRequest request, CancellationToken cancellationToken)
        {
            var result = await _contactApiClient.UpdateAsync(request, cancellationToken);

            return result.ActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _contactApiClient.DeleteAsync(id, cancellationToken);

            return result.ActionResult();
        }
    }
}
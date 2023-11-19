using ApiGateway.Extensions;
using ApiGateway.Models.ContactInformations;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationsController : ControllerBase
    {
        private readonly IContactInformationApiClient _contactInformationApiClient;

        public ContactInformationsController(IContactInformationApiClient contactInformationApiClient)
        {
            _contactInformationApiClient = contactInformationApiClient;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _contactInformationApiClient.GetContactInformationDetailAsync(id, cancellationToken);

            return result.ActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactInformationCreateRequest request, CancellationToken cancellationToken)
        {
            var result = await _contactInformationApiClient.CreateAsync(request: request, cancellationToken);

            return result.ActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _contactInformationApiClient.DeleteAsync(id, cancellationToken);

            return result.ActionResult();
        }
    }
}
using ApiGateway.Extensions;
using ApiGateway.Models.Reports;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportApiClient _reportApiClient;

        public ReportsController(IReportApiClient reportApiClient)
        {
            _reportApiClient = reportApiClient;
        }

        [HttpGet("GetLocationByReportId/{id:guid}")]
        public async Task<IActionResult> GetLocationByReportId(Guid id, CancellationToken cancellationToken)
        {
            var result = await _reportApiClient.GetLocationByReportIdAsync(id, cancellationToken);

            return result.ActionResult();
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            var result = await _reportApiClient.GetContactsAsync(cancellationToken);

            return result.ActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReportCreateRequest request, CancellationToken cancellationToken)
        {
            var result = await _reportApiClient.CreateAsync(request, cancellationToken);

            return result.ActionResult();
        }
    }
}
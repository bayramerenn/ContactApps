using Microsoft.AspNetCore.Mvc;
using ReportingService.Application.Features.Reports.Commands;
using ReportingService.Application.Features.Reports.Queries;

namespace ReportingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ApiControllerBase
    {
        [HttpGet("GetLocationByReportId/{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(new GetLocationByReportIdQuery(id), cancellationToken));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(new GetAllReportQuery(), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationReportCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
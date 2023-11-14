using Ardalis.GuardClauses;
using Event;
using Event.Models;
using Event.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportingService.Application.Common.Interfaces;
using ReportingService.Domain.Entities;
using ReportingService.Domain.Enums;

namespace ReportingService.Application.Features.Reports.Commands
{
    public record CreateLocationReportCommand(Guid? Id) : IRequest<Guid>;

    public class CreateLocationReportCommandHandler : IRequestHandler<CreateLocationReportCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IQueueService _queueService;

        public CreateLocationReportCommandHandler(IApplicationDbContext context, IQueueService queueService)
        {
            _context = context;
            _queueService = queueService;
        }

        public async Task<Guid> Handle(CreateLocationReportCommand request, CancellationToken cancellationToken)
        {
            Report? report;

            if (request.Id != null && request.Id != Guid.Empty)
            {
                report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

                Guard.Against.NotFound(request.Id.Value, report);

                report.Status = ReportStatus.InProgress;
            }
            else
            {
                report = new()
                {
                    Status = ReportStatus.InProgress
                };

                await _context.Reports.AddAsync(report);
            }

            await _context.SaveChangesAsync(cancellationToken);

            await _queueService.SendAsync(new CreateContractReportEvent(report.Id), QueueConstants.CREATE_CONRACT_REPORT);

            return report.Id;
        }
    }
}
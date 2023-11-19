using ReportingService.Domain.Enums;

namespace ReportingService.Application.Features.Reports.Queries
{
    public record GetAllReportResponse
    {
        public Guid Id { get; init; }
        public DateTime CreatedOn { get; init; }
        public ReportStatus Status { get; init; }
    }
}
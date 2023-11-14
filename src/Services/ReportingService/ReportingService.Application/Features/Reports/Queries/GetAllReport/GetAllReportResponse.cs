using ReportingService.Domain.Enums;

namespace ReportingService.Application.Features.Reports.Queries
{
    public record GetAllReportResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public ReportStatus Status { get; set; }
    }
}
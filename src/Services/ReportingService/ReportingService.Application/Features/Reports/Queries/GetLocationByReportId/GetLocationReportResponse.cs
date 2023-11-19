namespace ReportingService.Application.Features.Reports.Queries
{
    public record GetLocationReportResponse
    {
        public string Location { get; init; } = null!;
        public int ContractCount { get; init; }
        public int PhoneCount { get; init; }
    }
}
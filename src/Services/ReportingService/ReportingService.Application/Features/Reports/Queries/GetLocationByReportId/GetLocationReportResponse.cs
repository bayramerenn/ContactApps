namespace ReportingService.Application.Features.Reports.Queries
{
    public record GetLocationReportResponse
    {
        public string Location { get; set; } = null!;
        public int ContractCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
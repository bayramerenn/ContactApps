namespace ApiGateway.Models.Reports
{
    public record LocationReportResponse
    {
        public string Location { get; init; } = null!;
        public int ContractCount { get; init; }
        public int PhoneCount { get; init; }
    }
}
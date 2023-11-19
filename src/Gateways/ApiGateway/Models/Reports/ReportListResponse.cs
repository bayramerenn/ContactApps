namespace ApiGateway.Models.Reports
{
    public record ReportListResponse
    {
        public Guid Id { get; init; }
        public DateTime CreatedOn { get; init; }
        public string Status { get; init; }
    }
}
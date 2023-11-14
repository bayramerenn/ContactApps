namespace ContactDirectoryService.Application.Features.ContactInformations.Queries
{
    public record GetLocationReportResponse
    {
        public string Location { get; set; } = null!;
        public int ContractCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
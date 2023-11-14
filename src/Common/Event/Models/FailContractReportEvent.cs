namespace Event.Models
{
    public record FailContractReportEvent(Guid Id) : IEvent;
}
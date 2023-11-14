using ReportingService.Domain.Enums;
using Shared.BaseModels;

namespace ReportingService.Domain.Entities
{
    public class Report : BaseEntity<Guid>
    {
        public DateTime CreatedOn { get; set; }
        public ReportStatus Status { get; set; }
    }
}
using ApiGateway.Models.Reports;
using Shared.BaseModels;

namespace ApiGateway.Services
{
    public interface IReportApiClient
    {
        Task<ApiResponse<Guid>> CreateAsync(ReportCreateRequest request, CancellationToken cancellationToken);
        Task<ApiResponse<IEnumerable<ReportListResponse>>> GetContactsAsync(CancellationToken cancellationToken);
        Task<ApiResponse<IEnumerable<LocationReportResponse>>> GetLocationByReportIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
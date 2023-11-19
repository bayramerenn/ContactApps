using ApiGateway.Models.Reports;
using Shared.BaseModels;

namespace ApiGateway.Services
{
    public class ReportApiClient : IReportApiClient
    {
        private readonly HttpClient _apiClient;

        public ReportApiClient(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<IEnumerable<LocationReportResponse>>> GetLocationByReportIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _apiClient.GetAsync($"/api/Reports/GetLocationByReportId/{id}", cancellationToken);

            return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<LocationReportResponse>>>();
        }

        public async Task<ApiResponse<IEnumerable<ReportListResponse>>> GetContactsAsync(CancellationToken cancellationToken)
        {
            var response = await _apiClient.GetAsync($"/api/Reports/List", cancellationToken);

            return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<ReportListResponse>>>();
        }

        public async Task<ApiResponse<Guid>> CreateAsync(ReportCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _apiClient.PostAsJsonAsync("/api/Reports", request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        }
    }
}
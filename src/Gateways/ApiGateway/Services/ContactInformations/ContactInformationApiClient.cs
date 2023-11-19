using ApiGateway.Models;
using ApiGateway.Models.ContactInformations;
using Shared.BaseModels;
using System.Net;

namespace ApiGateway.Services
{
    public class ContactInformationApiClient : IContactInformationApiClient
    {
        private readonly HttpClient _apiClient;

        public ContactInformationApiClient(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<ContactInformationDetailResponse>> GetContactInformationDetailAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _apiClient.GetAsync($"/api/ContactInformations/{id}", cancellationToken);

            return await response.Content.ReadFromJsonAsync<ApiResponse<ContactInformationDetailResponse>>();
        }

        public async Task<ApiResponse<Guid>> CreateAsync(ContactInformationCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _apiClient.PostAsJsonAsync("/api/ContactInformations", request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _apiClient.DeleteAsync($"/api/ContactInformations/{id}", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new ApiResponse<bool>
                {
                    Data = true,
                    StatusCode = (int)response.StatusCode
                };
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        }
    }
}
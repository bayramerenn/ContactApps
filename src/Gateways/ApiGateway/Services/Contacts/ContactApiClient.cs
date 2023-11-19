using ApiGateway.Models;
using ApiGateway.Models.Contacts;
using Shared.BaseModels;
using System.Net;

namespace ApiGateway.Services
{
    public class ContactApiClient : IContactApiClient
    {
        private readonly HttpClient _apiClient;

        public ContactApiClient(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<ContactDetailResponse>> GetContactDetailAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _apiClient.GetAsync($"/api/Contacts/{id}", cancellationToken);

            return await response.Content.ReadFromJsonAsync<ApiResponse<ContactDetailResponse>>();
        }

        public async Task<ApiResponse<IEnumerable<ContactListResponse>>> GetContactsAsync(CancellationToken cancellationToken)
        {
            var response = await _apiClient.GetAsync($"/api/Contacts/List", cancellationToken);

            return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<ContactListResponse>>>();
        }

        public async Task<ApiResponse<Guid>> CreateAsync(ContactCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _apiClient.PostAsJsonAsync("/api/Contacts", request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        }

        public async Task<ApiResponse<bool>> UpdateAsync(ContactUpdateRequest request, CancellationToken cancellationToken)
        {
            var response = await _apiClient.PutAsJsonAsync("/api/Contacts", request, cancellationToken);

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

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _apiClient.DeleteAsync($"/api/Contacts/{id}", cancellationToken);

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
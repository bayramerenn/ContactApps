using ApiGateway.Models;
using ApiGateway.Models.Contacts;
using Shared.BaseModels;

namespace ApiGateway.Services
{
    public interface IContactApiClient
    {
        Task<ApiResponse<Guid>> CreateAsync(ContactCreateRequest request, CancellationToken cancellationToken);
        Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<ApiResponse<ContactDetailResponse>> GetContactDetailAsync(Guid id, CancellationToken cancellationToken);
        Task<ApiResponse<IEnumerable<ContactListResponse>>> GetContactsAsync(CancellationToken cancellationToken);
        Task<ApiResponse<bool>> UpdateAsync(ContactUpdateRequest request, CancellationToken cancellationToken);
    }
}
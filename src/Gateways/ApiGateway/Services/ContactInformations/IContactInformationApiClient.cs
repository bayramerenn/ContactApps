using ApiGateway.Models.ContactInformations;
using Shared.BaseModels;

namespace ApiGateway.Services
{
    public interface IContactInformationApiClient
    {
        Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<ApiResponse<ContactInformationDetailResponse>> GetContactInformationDetailAsync(Guid id, CancellationToken cancellationToken);
        Task<ApiResponse<Guid>> CreateAsync(ContactInformationCreateRequest request, CancellationToken cancellationToken);
    }
}

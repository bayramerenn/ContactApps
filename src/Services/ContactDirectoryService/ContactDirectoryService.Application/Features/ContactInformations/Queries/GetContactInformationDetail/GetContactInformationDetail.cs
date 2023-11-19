using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactDirectoryService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactDirectoryService.Application.Features.ContactInformations.Queries
{
    public record GetContactInformationDetailQuery(Guid Id) : IRequest<GetContactInformationDetailResponse>;

    public class GetContactInformationDetailHandler : IRequestHandler<GetContactInformationDetailQuery, GetContactInformationDetailResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContactInformationDetailHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetContactInformationDetailResponse> Handle(GetContactInformationDetailQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Id);

            var contactInformation = await _context
                .ContactInformations
                .ProjectTo<GetContactInformationDetailResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            Guard.Against.NotFound(request.Id, contactInformation);

            return contactInformation;
        }
    }
}
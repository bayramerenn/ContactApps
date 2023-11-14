using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactDirectoryService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactDirectoryService.Application.Features.Contacts.Queries
{
    public record GetContactListQuery : IRequest<IEnumerable<GetContactListResponse>>;

    public class ContactListQueryHandler : IRequestHandler<GetContactListQuery, IEnumerable<GetContactListResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetContactListResponse>> Handle(GetContactListQuery request, CancellationToken cancellationToken)
        {
            return await _context
               .Contacts
               .AsNoTracking()
               .ProjectTo<GetContactListResponse>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
        }
    }
}
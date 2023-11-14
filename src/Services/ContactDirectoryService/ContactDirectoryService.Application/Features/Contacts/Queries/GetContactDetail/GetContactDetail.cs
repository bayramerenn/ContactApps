using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactDirectoryService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactDirectoryService.Application.Features.Contacts.Queries
{
    public record GetContactDetailQuery(Guid Id) : IRequest<GetContactDetailResponse>;

    public class ContactDetailQueryHandler : IRequestHandler<GetContactDetailQuery, GetContactDetailResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetContactDetailResponse> Handle(GetContactDetailQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Id);

            var contact = await _context
               .Contacts
               .Include(c => c.ContactInformations)
               .AsNoTracking()
               .ProjectTo<GetContactDetailResponse>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            Guard.Against.NotFound(request.Id, contact);

            return contact;
        }
    }
}
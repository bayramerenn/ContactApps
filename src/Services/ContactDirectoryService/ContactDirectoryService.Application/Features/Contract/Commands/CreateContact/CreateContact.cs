using AutoMapper;
using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Common.Interfaces;
using ContactDirectoryService.Domain.Entities;
using MediatR;
using Shared.Constants;

namespace ContactDirectoryService.Application.Features.Contract.Commands.CreateContact
{
    public record CreateContactCommand(string FirstName, string LastName, string Company) : IRequest<Guid>, ICacheRemoverRequest
    {
        public bool BypassCache => false;

        public string CacheKey => CacheKeyConstant.RemoveReportKey;
    }

    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            Contact contact = _mapper.Map<Contact>(request);

            await _context.Contacts.AddAsync(contact, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return contact.Id;
        }
    }
}
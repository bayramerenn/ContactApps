using Ardalis.GuardClauses;
using AutoMapper;
using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Common.Interfaces;
using ContactDirectoryService.Domain.Entities;
using ContactDirectoryService.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Exceptions;

namespace ContactDirectoryService.Application.Features.ContactInformations.Command
{
    public record CreateContactInformationCommand(Guid ContactId, ContactType ContactType, string Content) : IRequest<Guid>, ICacheRemoverRequest
    {
        public bool BypassCache => false;

        public string CacheKey => CacheKeyConstant.RemoveReportKey;
    }

    public class CreateContactInformationCommandHandler : IRequestHandler<CreateContactInformationCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateContactInformationCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateContactInformationCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context
                .Contacts
                .Include(c => c.ContactInformations)
                .FirstOrDefaultAsync(contact => contact.Id == request.ContactId, cancellationToken);

            Guard.Against.NotFound(request.ContactId, contact);

            var isDupplicateEntity = contact
                .ContactInformations?
                .Any(ci => ci.ContactType == request.ContactType && ci.Content == request.Content);

            if (isDupplicateEntity != null && isDupplicateEntity.Value)
            {
                throw new DuplicateException($"This data has been recorded for this person before. Content={request.Content}");
            }

            var contactInformation = _mapper.Map<ContactInformation>(request);

            await _context.ContactInformations.AddAsync(contactInformation, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return contactInformation.Id;
        }
    }
}
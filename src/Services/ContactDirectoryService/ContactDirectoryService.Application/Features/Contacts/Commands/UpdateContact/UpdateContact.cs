using Ardalis.GuardClauses;
using AutoMapper;
using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Common.Interfaces;
using MediatR;
using Shared.Constants;

namespace ContactDirectoryService.Application.Features.Contacts.Commands
{
    public record UpdateContactCommand(Guid Id, string FirstName, string LastName, string Company) : IRequest<Unit>, ICacheRemoverRequest
    {
        public bool BypassCache => false;

        public string CacheKey => CacheKeyConstant.RemoveReportKey;
    }

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateContactCommandHandler(IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var oldContact = await _context.Contacts.FindAsync(request.Id, cancellationToken);

            Guard.Against.NotFound(request.Id, oldContact);

            _mapper.Map(request, oldContact);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
using Ardalis.GuardClauses;
using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Common.Interfaces;
using MediatR;
using Shared.Constants;

namespace ContactDirectoryService.Application.Features.ContactInformations.Command
{
    public record DeleteContactInformationCommand(Guid Id) : IRequest<Unit>, ICacheRemoverRequest
    {
        public bool BypassCache => false;

        public string CacheKey => CacheKeyConstant.RemoveReportKey;
    }

    public class DeleteContactInformationCommandHandler : IRequestHandler<DeleteContactInformationCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteContactInformationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteContactInformationCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Id);

            var contactInformation = await _context.ContactInformations
                .FindAsync(request.Id, cancellationToken);

            Guard.Against.NotFound(request.Id, contactInformation);

            _context.ContactInformations.Remove(contactInformation);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
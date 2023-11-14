using Ardalis.GuardClauses;
using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Common.Interfaces;
using MediatR;
using Shared.Constants;

namespace ContactDirectoryService.Application.Features.Contract.Commands.DeleteContact
{
    public record DeleteContactCommand(Guid Id) : IRequest<Unit>, ICacheRemoverRequest
    {
        public bool BypassCache => false;

        public string CacheKey => CacheKeyConstant.RemoveReportKey;
    }

    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteContactCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Id);

            var contact = await _context.Contacts.FindAsync(request.Id, cancellationToken);

            Guard.Against.NotFound(request.Id, contact);

            _context.Contacts.Remove(contact);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
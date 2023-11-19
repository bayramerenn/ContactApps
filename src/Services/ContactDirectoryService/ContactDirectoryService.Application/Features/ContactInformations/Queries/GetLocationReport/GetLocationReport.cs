using ContactDirectoryService.Application.Common.Behaviours.Caching;
using ContactDirectoryService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Enums;

namespace ContactDirectoryService.Application.Features.ContactInformations.Queries
{
    public record GetLocationReportQuery(Guid ReportId) : IRequest<IEnumerable<GetLocationReportResponse>>, ICachableRequest
    {
        public bool BypassCache => false;

        public string CacheKey => CacheKeyConstant.GetReportKey(ReportId);

        public TimeSpan? SlidingExpiration => null;
    }

    public class GetLocationReportQueryHandler : IRequestHandler<GetLocationReportQuery, IEnumerable<GetLocationReportResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetLocationReportQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetLocationReportResponse>> Handle(GetLocationReportQuery request, CancellationToken cancellationToken)
        {
            var result = await (
                     from c in _context.ContactInformations
                     where c.ContactType == ContactType.Location
                     let phoneCount = _context.ContactInformations
                                         .Count(p => p.ContactType == ContactType.Phone && p.ContactId == c.ContactId)
                     group new { c, phoneCount } by c.Content into groupedData
                     select new GetLocationReportResponse
                     {
                         Location = groupedData.Key,
                         ContractCount = groupedData.Count(),
                         PhoneCount = groupedData.Sum(item => item.phoneCount)
                     }
                 ).ToListAsync(cancellationToken);

            return result;
        }
    }
}
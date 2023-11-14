using Ardalis.GuardClauses;
using MediatR;
using Shared.CacheService;
using Shared.Constants;

namespace ReportingService.Application.Features.Reports.Queries
{
    public record GetLocationByReportIdQuery(Guid Id) : IRequest<IEnumerable<GetLocationReportResponse>>;

    public class GetLocationByReportIdQueryHandler : IRequestHandler<GetLocationByReportIdQuery, IEnumerable<GetLocationReportResponse>>
    {
        private readonly IRedisCache _cache;

        public GetLocationByReportIdQueryHandler(IRedisCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<GetLocationReportResponse>> Handle(GetLocationByReportIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeyConstant.GetReportKey(request.Id);

            var response = await _cache.GetAsync<IEnumerable<GetLocationReportResponse>>(cacheKey);

            Guard.Against.NotFound(request.Id, response);

            return response;
        }
    }
}
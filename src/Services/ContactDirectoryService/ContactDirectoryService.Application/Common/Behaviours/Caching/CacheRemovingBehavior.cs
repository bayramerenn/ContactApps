using MediatR;
using Shared.CacheService;

namespace ContactDirectoryService.Application.Common.Behaviours.Caching
{
    public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheRemoverRequest
    {
        private readonly IRedisCache _cache;

        public CacheRemovingBehavior(IRedisCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.BypassCache) return await next();

            TResponse response = await next();

            if (request is ICacheRemoverRequest cacheRemoverRequest)
            {
                await _cache.RemovePatternAsync(cacheRemoverRequest.CacheKey);
            }

            return response;
        }
    }
}
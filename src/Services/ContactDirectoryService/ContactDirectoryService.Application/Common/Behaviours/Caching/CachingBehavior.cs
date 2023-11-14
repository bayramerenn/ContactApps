using MediatR;
using Microsoft.Extensions.Options;
using Shared.CacheService;

namespace ContactDirectoryService.Application.Common.Behaviours.Caching
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICachableRequest
    {
        private readonly IRedisCache _cache;
        private readonly CacheSettings _cacheSettings;

        public CachingBehavior(IRedisCache cache, IOptions<CacheSettings> optionsSnapshot)
        {
            _cache = cache;
            _cacheSettings = optionsSnapshot.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICachableRequest cacheableRequest)
            {
                TResponse? response;

                if (cacheableRequest.BypassCache) return await next();

                response = await _cache.GetAsync<TResponse>(cacheableRequest.CacheKey);

                if (response == null)
                    response = await GetResponseAndAddToCache(next, cacheableRequest, request);

                return response;
            }
            else
            {
                return await next();
            }
        }

        private async Task<TResponse> GetResponseAndAddToCache(RequestHandlerDelegate<TResponse> next, ICachableRequest cacheableQuery, TRequest request)
        {
            var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromSeconds(_cacheSettings.SlidingExpiration);

            var response = await next();
            await _cache.SaveAsync(cacheableQuery.CacheKey, response, slidingExpiration);
            return response;
        }
    }
}
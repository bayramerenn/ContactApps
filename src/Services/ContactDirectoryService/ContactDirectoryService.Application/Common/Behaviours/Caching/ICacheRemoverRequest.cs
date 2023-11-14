namespace ContactDirectoryService.Application.Common.Behaviours.Caching
{
    public interface ICacheRemoverRequest
    {
        bool BypassCache { get; }
        string CacheKey { get; }
    }
}
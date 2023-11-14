namespace Shared.Constants
{
    public static class CacheKeyConstant
    {
        public const string RemoveReportKey = "reports*";

        public static string GetReportKey(Guid reportId) =>
               string.Format("reports:{0}", reportId);
    }
}
using ApiGateway.Services;

namespace ApiGateway.Extensions
{
    public static class ConfigureService
    {
        public static void AddConfigureHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IContactApiClient, ContactApiClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["ContactDirectoryServiceUrl"]!);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddHttpClient<IContactInformationApiClient, ContactInformationApiClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["ContactDirectoryServiceUrl"]!);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddHttpClient<IReportApiClient, ReportApiClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["ReportingServiceUrl"]!);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));
        }
    }
}
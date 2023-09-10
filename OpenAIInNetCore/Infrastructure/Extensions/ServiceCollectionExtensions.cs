using InstantBedtimeStory.Services;

namespace InstantBedtimeStory.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IOpenAIConnector, OpenAIConnector>();
        }

    }
}

using QnAWithOpenAIEmbedding.Services;

namespace QnAWithOpenAIEmbedding.Infrastructures.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IOpenAIConnector, OpenAIConnector>();
        }

    }
}

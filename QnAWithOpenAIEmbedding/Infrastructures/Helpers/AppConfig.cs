using System.Reflection;

namespace QnAWithOpenAIEmbedding.Infrastructures.Helpers
{
    public static class AppConfig
    {
        public static IConfigurationRoot CurrentConiguration 
        { 
            get
            {
                var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddUserSecrets(Assembly.GetExecutingAssembly())
                        .AddEnvironmentVariables();

                return builder.Build();
            }
        }
    }
}

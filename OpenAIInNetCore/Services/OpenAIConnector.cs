using InstantBedtimeStory.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using OpenAI_API;

namespace InstantBedtimeStory.Services
{
    public class OpenAIConnector : IOpenAIConnector
    {
        private readonly OpenAIAPI _apiClient;
        private readonly IHubContext<GptResponseHub> _hubContext;

        public OpenAIConnector(IConfiguration configuration, IHubContext<GptResponseHub> hubContext)
        {
            _apiClient = new OpenAIAPI(configuration["OpenAI:APIKey"]);
            _hubContext = hubContext;
        }

        public Task GetStoryAsync(string topic)
        {
            Task.Run(async () =>
            {
                var gptConversation = _apiClient.Chat.CreateConversation();

                /// give instruction as System
                gptConversation.AppendSystemMessage("User enters a topic and you need to respond with a short bed time story on that topic. Story is for kids so content needs to be appropriate for kids.");
                // now let's give a topic name
                gptConversation.AppendUserInput(topic);
                await foreach (var message in gptConversation.StreamResponseEnumerableFromChatbotAsync())
                {
                    await _hubContext.Clients.All.SendAsync("gptresponse", message);
                }
            });

            return Task.CompletedTask;
        }
    }
}

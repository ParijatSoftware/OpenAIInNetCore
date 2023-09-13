using Microsoft.AspNetCore.SignalR;
using OpenAI_API;
using QnAWithOpenAIEmbedding.Infrastructures.Helpers;
using QnAWithOpenAIEmbedding.Infrastructures.Hubs;
using QnAWithOpenAIEmbedding.Models;
using SharpToken;

namespace QnAWithOpenAIEmbedding.Services
{
    public class OpenAIConnector : IOpenAIConnector
    {
        private readonly OpenAIAPI _apiClient;
        private readonly IHubContext<GptResponseHub> _hubContext;
        private readonly List<TrainingDataset> _trainingDatasets;

        public OpenAIConnector(IConfiguration configuration, IHubContext<GptResponseHub> hubContext)
        {
            _apiClient = new OpenAIAPI(configuration["OpenAI:APIKey"]);
            _hubContext = hubContext;
            _trainingDatasets = FileHelpers.GetEmbeddingContent();
        }

        public Task AskAsync(string query)
        {
            Task.Run(async () =>
            {
                var gptConversation = _apiClient.Chat.CreateConversation();
                gptConversation.Model = OpenAI_API.Models.Model.GPT4;
                gptConversation.RequestParameters.MaxTokens = 500;

                var queryMessage = await BuildQueryMessageAsync(query, "gpt-4", 8192 - 500);

                gptConversation.AppendSystemMessage("You answer questions about the 2022 Winter Olympics.");
                gptConversation.AppendUserInput(queryMessage);

                await foreach (var message in gptConversation.StreamResponseEnumerableFromChatbotAsync())
                {
                    await _hubContext.Clients.All.SendAsync("gptresponse", message);
                }
            });

            return Task.CompletedTask;
        }


        private async Task<string> BuildQueryMessageAsync(string query, string model, int tokenBudget)
        {
            var relatedTexts = await SearchHelper.StringsRankedByRelatedness(query, _trainingDatasets);
            var introduction = "Use the below articles on the 2022 Winter Olympics to answer the subsequent question. If the answer cannot be found in the articles, write \"I could not find an answer.\"";
            var question = $"\n\nQuestion: {query}";
            var message = introduction;

            foreach (var text in relatedTexts)
            {
                var nextArticle = $"\n\nWikipedia article section:\n\"\"\"\n{text.Text}\n\"\"\"";
                if (NumTokens(message + nextArticle + question, model) > tokenBudget)
                    break;
                else
                    message += nextArticle;
            }
            return message + question;
        }

        private static int NumTokens(string text, string model)
        {
            var encoding = GptEncoding.GetEncodingForModel(model);
            var encodedText = encoding.Encode(text);
            return encodedText.Count;
        }

    }
}

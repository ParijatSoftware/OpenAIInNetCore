namespace QnAWithOpenAIEmbedding.Services
{
    public interface IOpenAIConnector
    {
        Task AskAsync(string query);
    }
}

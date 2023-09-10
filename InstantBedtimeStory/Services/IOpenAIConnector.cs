namespace InstantBedtimeStory.Services
{
    public interface IOpenAIConnector
    {
        Task GetStoryAsync(string topic);
    }
}

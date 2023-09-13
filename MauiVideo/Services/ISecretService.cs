namespace MauiVideo.Services
{
    public interface ISecretService
    {
        Task<string> RetrieveApiKey();
    }
}
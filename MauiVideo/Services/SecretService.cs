using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiVideo.Services
{
    /// <summary>
    /// Service to read the ApiKey from a local text file
    /// Get your own API Key here https://developer.themoviedb.org/docs
    /// </summary>
    public class SecretService : ISecretService
    {
        string apiKey { get; set; } = String.Empty;

        public async Task<string> RetrieveApiKey()
        {
            if (String.IsNullOrWhiteSpace(apiKey))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(TmdbUrls.ApiKeyPath);
                using var reader = new StreamReader(stream);

                apiKey = reader.ReadToEnd();
            }

            return apiKey;
        }
    }
}

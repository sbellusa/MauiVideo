using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiVideo.Http
{
    public static class HttpServiceExtensions
    {
        public static async Task<T> ProcessAsync<T>(this HttpClient client, RequestBase<T> request) where T : ResponseBase
        {
            ResponseBase response = null;

            if (client == null) throw new ArgumentNullException(nameof(client));
            if (request == null) throw new ArgumentNullException(nameof(request));

            switch (request.Method)
            {
                case RequestMethods.Get:
                    response = await ProcessHttpGetRequest(client, request);
                    break;

                case RequestMethods.Post:
                    await PostAsync(client, request);
                    break;

                case RequestMethods.Put:
                    await PutAsync(client, request);
                    break;

                case RequestMethods.Delete:
                    await DeleteAsync(client, request);
                    break;
            }

            return (T)response;
        }

        #region GET

        private async static Task<ResponseBase> ProcessHttpGetRequest(HttpClient client, RequestBase request)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(0);
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, request.Url);

                response = await client.SendAsync(httpRequest);

                int statusCode = (int)response.StatusCode;

                var stream = await response.Content.ReadAsStreamAsync();

                ResponseBase result = (ResponseBase)JsonSerializer.Deserialize(stream, request.ResponseType);
                if (result != null)
                {
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    result.StatusCode = statusCode;

                    return result;
                }
                else
                {
                    return GetDefaultResponse(request.ResponseType, response.IsSuccessStatusCode, statusCode);
                }

            }
            catch
            {
                return GetDefaultResponse(request.ResponseType, false, 0);
            }
        }
        #endregion

        #region POST
        private static Task PostAsync(HttpClient client, RequestBase request)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region PUT
        private static Task PutAsync(HttpClient client, RequestBase request)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DELETE
        private static Task DeleteAsync(HttpClient client, RequestBase request)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Other

        private static ResponseBase GetDefaultResponse(Type type, bool isSuccessStatusCode, int statusCode)
        {
            var result = (Activator.CreateInstance(type) as ResponseBase);
            result.IsSuccessStatusCode = isSuccessStatusCode;
            result.StatusCode = statusCode;

            return result;
        }

        #endregion
    }
}

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class BaseApi : IAsyncApi
    {
        private readonly HttpClient _httpClient;

        public BaseApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(Uri resourceUri)
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, resourceUri))
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }
        }
    }

    public interface IAsyncApi
    {
        /// <summary>
        ///     Sends an API request async using GET Method
        /// </summary>
        /// <param name="resourceUri">The resouce uri path</param>
        /// <returns>Asyncronous result turns by TApiResouce</returns>
        Task<T> GetAsync<T>(Uri resourceUri);
    }
}

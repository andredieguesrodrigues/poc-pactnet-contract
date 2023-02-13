using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ReadMe.Consumer
{
    public class UserApiClient
    {
        private readonly HttpClient client;

        public UserApiClient(Uri baseUri = null)
        {
            this.client = new HttpClient { BaseAddress = baseUri ?? new Uri("http://my-api") };
        }

        public async Task<User> GetUser(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/users/" + id);
            request.Headers.Add("Accept", "application/json");

            var response = await this.client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            var status = response.StatusCode;

            string reasonPhrase = response.ReasonPhrase;

            request.Dispose();
            response.Dispose();

            if (status == HttpStatusCode.OK)
            {
                return !string.IsNullOrEmpty(content)
                           ? JsonConvert.DeserializeObject<User>(content)
                           : null;
            }

            throw new Exception(reasonPhrase);
        }
    }
}

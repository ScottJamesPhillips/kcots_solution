using Kcots.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Utilities
{
    class HttpClientWrapper:IHttpClientWrapper
    {
        private readonly HttpClient httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                // Delegate the actual work to the HttpClient class
                return await httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions or log them as needed
                Console.WriteLine($"HttpRequestException: {ex.Message}");
                throw;
            }
        }
    }
}

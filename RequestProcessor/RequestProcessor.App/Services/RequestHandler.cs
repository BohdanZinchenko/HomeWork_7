using System;
using System.Net.Http;

using System.Threading.Tasks;
using RequestProcessor.App.Models;
using RequestProcessor.App.Services;


namespace RequestProcessor.App
{
    internal class RequestHandler:IRequestHandler
    {
        private readonly HttpClient _client;
        public RequestHandler(HttpClient client)
        {
            _client = client;
        }
        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            if (requestOptions == null) throw new ArgumentNullException();
            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException();
            using var message =
                new HttpRequestMessage(MapMethod(requestOptions.Method), new Uri(requestOptions.Address));

            Console.WriteLine($"Created message of current {Task.CurrentId} task");

            
            using var response = await _client.SendAsync(message);

            if (_client.Timeout.Minutes > 10)
            {
                return new Response()
                {
                    Code = -1,
                    Content = "TimeOutError",
                    Handled = true

                };
            }
            
            using var content = response.Content;

            try
            {
                
                return new Response()
                {
                    Code = Convert.ToInt32(response.StatusCode),
                    Content = await content.ReadAsStringAsync(),
                    Handled = true
                    
                };
            }
            catch
            {
                
                return new Response()
                {
                    Code = Convert.ToInt32(response.StatusCode),
                    Content = $"Code not 2xx\n {response.Headers.ToString()}",
                    Handled = true
                    
                };
            }
            

        }

        private static HttpMethod MapMethod(RequestMethod method)
        {
            return method switch
            {
                RequestMethod.Undefined => throw new ArgumentOutOfRangeException(),
                RequestMethod.Get => HttpMethod.Get,
                RequestMethod.Post => HttpMethod.Post,
                RequestMethod.Put => HttpMethod.Put,
                RequestMethod.Patch => HttpMethod.Patch,
                RequestMethod.Delete => HttpMethod.Delete,
                _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
            };
        }
    }
}
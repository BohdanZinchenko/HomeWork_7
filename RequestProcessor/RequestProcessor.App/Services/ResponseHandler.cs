using System;
using System.IO;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    internal class ResponseHandler:IResponseHandler
    {
       
        public Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            if (requestOptions == null) throw new ArgumentException();
            if (responseOptions == null) throw new ArgumentException();
            Console.WriteLine("Create file with response");
                return File.WriteAllTextAsync(responseOptions.Path,
                    $"Status Code = {response.Code}\n{response.Content}");

        }
    }
}
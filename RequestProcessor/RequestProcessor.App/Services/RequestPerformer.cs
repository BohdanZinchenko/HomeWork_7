using System;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        private readonly ILogger _logger;
        private readonly IResponseHandler _responseHandler;
        private readonly IRequestHandler _requestHandler;
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        public RequestPerformer(
            IRequestHandler requestHandler, 
            IResponseHandler responseHandler,
            ILogger logger)
        {
            _responseHandler = responseHandler;
            _requestHandler = requestHandler;
            _logger = logger;
        }

        

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(
            IRequestOptions requestOptions, 
            IResponseOptions responseOptions)
        {
            Console.WriteLine("Work Task"+Task.CurrentId);
            if (requestOptions == null) throw new ArgumentNullException();
            if (responseOptions == null) throw new ArgumentNullException();
            try
            {
                var response = await _requestHandler.HandleRequestAsync(requestOptions);
                _logger.Log($"Handled Request");
                if (response == null)
                {
                    var nullResponse = new Response
                    {
                        Code = 0,
                        Content = "",
                        Handled = true
                    };
                    await _responseHandler.HandleResponseAsync(nullResponse, requestOptions, responseOptions);
                    return false;
                }

                await _responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
                _logger.Log($"Handled Response");

                return true;
            }
            catch (PerformException e)
            {

            }

            return false;
        }
    }
}

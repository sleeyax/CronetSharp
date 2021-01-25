using System;
using CronetSharp;
using CronetSharp.Cronet;

namespace example.Examples
{
    public class GetRequestExample : IExample
    {
        private UrlRequest _getRequest;
        
        public void Run(CronetEngine engine)
        {
            // create default executor
            var executor = new Executor();

            // create callback
            var myUrlRequestCallback = new UrlRequestCallback(new ExampleCallBackHandler());

            // Create and configure a UrlRequest object
            // send GET request
            var getRequestParams = new UrlRequestParams
            {
                HttpMethod = "GET",
                Priority = RequestPriority.Highest,
                Headers = new []
                {
                    new HttpHeader("user-agent", "mycustomuseragent"),
                    new HttpHeader("accept", "*/*"),
                    new HttpHeader("cookie", "abc=def; foo=bar"),
                    // new HttpHeader("cookie", "foo=bar")
                }
            };
            _getRequest = engine.NewUrlRequest("https://httpbin.org/anything", myUrlRequestCallback, executor, getRequestParams);
            getRequestParams.Dispose();
            
            Console.WriteLine("Starting GET request...");
            _getRequest.Start();
        }

        public void Dispose() => _getRequest?.Dispose();
    }
}
using System;
using System.Collections.Generic;
using CronetSharp;

namespace example.Examples
{
    public class ProxyRequestExample : IExample
    {
        private readonly Proxy _proxy;

        public ProxyRequestExample(Proxy proxy)
        {
            _proxy = proxy;
        }
        
        private UrlRequest _proxyRequest;
        
        public void Run(CronetEngine engine)
        {
            // don't use given engine, we'll construct our own proxy engine
            engine.Shutdown();
            engine.Dispose();
            engine = new CronetEngine(new CronetEngineParams
            {
                Proxy = _proxy
            });
            engine.Start();
            
            // create default executor & callback
            var executor = new Executor();
            var myUrlRequestCallback = new ExampleCallBack();
            
            // target url
            var uri = new Uri("https://httpbin.org/anything");

            // build headers based on proxy settings
            var headers = new List<HttpHeader>
            {
                new HttpHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36"),
                new HttpHeader("host", $"{uri.Host}:{uri.Port}")
            };
            
            // Create and configure a UrlRequest object with proxy
            var requestParams = new UrlRequestParams
            {
                HttpMethod = "GET",
                Headers = headers.ToArray(),
                // Proxy = _proxy
            };
            _proxyRequest = engine.NewUrlRequest(uri.ToString(), myUrlRequestCallback, executor, requestParams);
            requestParams.Dispose();
            
            Console.WriteLine("Starting proxied GET request...");
            _proxyRequest.Start();
        }

        public void Dispose() => _proxyRequest?.Dispose();
    }
}
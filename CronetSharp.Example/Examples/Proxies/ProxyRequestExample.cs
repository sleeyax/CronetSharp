using System;
using System.Collections.Generic;
using CronetSharp;

namespace example.Examples.Proxies
{
    public class ProxyRequestExample : IExample
    {
        private UrlRequest _proxyRequest;
        
        public void Run(CronetEngine engine)
        {
            // create default executor & callback
            var executor = new Executor();
            var myUrlRequestCallback = new UrlRequestCallback(new ExampleCallBackHandler());
            
            // target uri
            var uri = new Uri("https://httpbin.org/anything");
            string host = $"{uri.Host}:{uri.Port}";

            // our proxy to use
            var proxy = new Proxy("127.0.0.1", 8887);

            // build headers based on proxy settings
            var headers = new List<HttpHeader>
            {
                new HttpHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36"),
                new HttpHeader("host", host)
            };
            if (proxy.IsAuthenticated)
                headers.Add(new HttpHeader("Proxy-Authorization", $"Basic {proxy.EncodeBasic()}"));
            
            // TODO: find out a way to use proxies :/
            // Create and configure a UrlRequest object
            // send CONNECT request
            var requestParams = new UrlRequestParams
            {
                HttpMethod = "CONNECT",
                Headers = headers.ToArray()
            };
            _proxyRequest = engine.NewUrlRequest($"http://{proxy.Address}:{proxy.Port}/abc", myUrlRequestCallback, executor, requestParams);
            requestParams.Dispose();
            
            Console.WriteLine("Starting CONNECT request...");
            _proxyRequest.Start();
        }

        public void Dispose() => _proxyRequest?.Dispose();
    }
}
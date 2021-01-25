using System;
using CronetSharp;

namespace example.Examples
{
    public class PostRequestExample : IExample
    {
        private UrlRequest _postRequest;
        
        public void Run(CronetEngine engine)
        {
            // create default executor
            var executor = new Executor();
            
            var myUrlRequestCallback = new UrlRequestCallback(new ExampleCallBackHandler());
            
            var postRequestBuilder = engine.NewUrlRequestBuilder("https://httpbin.org/anything", myUrlRequestCallback, executor)
                .SetHttpMethod("POST")
                .AddHeader("content-type", "application/json")
                .AddHeader("my-custom-header", "customvalue")
                .SetUploadDataProvider(UploadDataProvider.Create("{}"), executor);
            _postRequest = postRequestBuilder.Build();
            postRequestBuilder.GetParams().Dispose(); // free up now unnecessary resources from unmanaged memory
            
            Console.WriteLine("Starting POST request...");
            _postRequest.Start();
        }

        public void Dispose() => _postRequest?.Dispose();
    }
}
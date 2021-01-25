using System;
using CronetSharp;

namespace example
{
    public class ExampleCallBackHandler : UrlRequestCallbackHandler
    {
        public ExampleCallBackHandler()
        {
            OnRedirectReceived = (request, info, arg3) =>
            {
                Console.WriteLine("-> redirect received");
                request.FollowRedirect();
            };
            OnResponseStarted = (request, info) =>
            {
                Console.WriteLine("-> response started");
                request.Read(ByteBuffer.Allocate(102400));
            };
            OnReadCompleted = (request, info, byteBuffer, bytesRead) =>
            {
                Console.WriteLine("-> read completed");
                Console.WriteLine(byteBuffer.GetDataAsString());
                byteBuffer.Clear();
                request.Read(byteBuffer);
            };
            OnSucceeded = (request, info) =>
            {
                Console.WriteLine("-> succeeded");
                Console.WriteLine($"Negotiated protocol: {info.NegotiatedProtocol}");
                Console.WriteLine($"Response Status code: {info.HttpStatusCode}");
                Console.WriteLine($"Response Headers: ");
                foreach (var header in info.Headers)
                    Console.WriteLine($"{header.Name}:{header.Value}");
                Console.WriteLine($"Response bytes received: {info.ReceivedByteCount}");
            };
            OnFailed = (request, info, error) => Console.WriteLine("-> failed");
            OnCancelled = (request, info) => Console.WriteLine("-> canceled");
        }
    }
}
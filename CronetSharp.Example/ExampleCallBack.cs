using System;
using System.Text;
using CronetSharp;

namespace example
{
    public class ExampleCallBack : UrlRequestCallback
    {
        public ExampleCallBack()
        {
            OnRedirectReceived = (request, info, arg3) =>
            {
                Console.WriteLine("-> redirect received");
                request.FollowRedirect();
            };
            OnResponseStarted = (request, info) =>
            {
                Console.WriteLine("-> response started");

                ulong contentLength = 102400;
                HttpHeader httpHeaderContentLength =
                  ExampleCallBack.GetHttpHeaderByName(info.Headers, "content-length");
                if (httpHeaderContentLength != null)
                {
                    contentLength = UInt32.Parse(httpHeaderContentLength.Value);
                }

                request.Read(ByteBuffer.Allocate(contentLength));
            };

            OnReadCompleted = (request, info, byteBuffer, bytesRead) =>
            {
                Console.WriteLine("-> read completed");

                byte[] data = byteBuffer.GetData();
                string strData = UTF8Encoding.UTF8.GetString(data, 0, (int)bytesRead);
                Console.WriteLine(strData);
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

        private static HttpHeader GetHttpHeaderByName(HttpHeader[] headers, string headerName)
        {
            foreach (HttpHeader header in headers)
            {
                var comparison = StringComparison.InvariantCultureIgnoreCase;
                if (header.Name.Equals(headerName, comparison))
                {
                    return header;
                }
            }

            return null;
        }
    }
}
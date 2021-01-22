using System;
using System.Collections.Generic;

namespace CronetSharp
{
    public class UrlResponseInfo
    {
        private readonly IntPtr _urlResponseInfoPtr;

        public UrlResponseInfo()
        {
            _urlResponseInfoPtr = Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_Create();
        }
        
        public UrlResponseInfo(IntPtr urlResponseInfoPtr)
        {
            _urlResponseInfoPtr = urlResponseInfoPtr;
        }

        /// <summary>
        /// Returns an unmodifiable map of the response-header fields and values.
        /// Each list of values for a single header field is in the same order they were received over the wire.
        /// </summary>
        /// <value></value>
        public IList<HttpHeader> Headers
        {
            get
            { 
                var headers = new List<HttpHeader>();
                
                var size = Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_all_headers_list_size(_urlResponseInfoPtr);
                for (uint i = 0; i < size; i++)
                {
                    var httpHeaderPtr = Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_all_headers_list_at(_urlResponseInfoPtr, i);
                    headers.Add(new HttpHeader(httpHeaderPtr));
                }

                return headers;
            }
            set
            {
                foreach (var header in value)
                    Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_all_headers_list_add(_urlResponseInfoPtr, header.Pointer);
            }
        }

        /// <summary>
        /// Returns the HTTP status code.
        /// When a resource is retrieved from the cache, whether it was revalidated or not, the original status code is returned.
        /// </summary>
        /// <value></value>
        public int HttpStatusCode
        {
            get => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_http_status_code_get(_urlResponseInfoPtr);
            set => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_http_status_code_set(_urlResponseInfoPtr, value);
        }

        /// <summary>
        /// Returns the HTTP status text of the status line.
        /// For example, if the request received a "HTTP/1.1 200 OK" response, this method returns "OK".
        /// </summary>
        /// <value></value>
        public string HttpStatusCodeText
        {
            get => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_http_status_text_get(_urlResponseInfoPtr);
            set => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_http_status_text_set(_urlResponseInfoPtr, value);
        }

        /// <summary>
        /// Returns the protocol (for example 'quic/1+spdy/3') negotiated with the server.
        /// Returns an empty string if no protocol was negotiated, the protocol is not known, or when using plain HTTP or HTTPS.
        /// </summary>
        /// <value></value>
        public string NegotiatedProtocol
        {
            get => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_negotiated_protocol_get(_urlResponseInfoPtr);
            set => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_negotiated_protocol_set(_urlResponseInfoPtr, value);
        }

        /// <summary>
        /// Returns the proxy server that was used for the request.
        /// </summary>
        /// <value></value>
        public string ProxyServer
        {
            get => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_proxy_server_get(_urlResponseInfoPtr);
            set => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_proxy_server_set(_urlResponseInfoPtr, value);
        }

        /// <summary>
        /// Returns a minimum count of bytes received from the network to process this request.
        /// This count may ignore certain overheads (for example IP and TCP/UDP framing, SSL handshake and framing, proxy handling).
        /// This count is taken prior to decompression (for example GZIP) and includes headers and data from all redirects.
        /// This value may change (even for one UrlResponseInfo instance) as the request progresses until completion, when onSucceeded(), onFailed(), or onCanceled() is called.
        /// </summary>
        /// <value></value>
        public long ReceivedByteCount
        {
            get => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_received_byte_count_get(_urlResponseInfoPtr);
            set => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_received_byte_count_set(_urlResponseInfoPtr, value);
        }

        /// <summary>
        /// Returns the URL the response is for.
        /// This is the URL after following redirects, so it may not be the originally requested URL.
        /// </summary>
        /// <value></value>
        public string Url
        {
            get => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_url_get(_urlResponseInfoPtr);
            set => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_url_set(_urlResponseInfoPtr, value);
        }

        /// <summary>
        /// Returns the URL chain.
        /// The first entry is the originally requested URL; the following entries are redirects followed.
        /// </summary>
        /// <value></value>
        public IList<string> UrlChain
        {
            get
            {
                var chains = new List<string>();
                var size = Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_url_chain_size(_urlResponseInfoPtr);
                for (uint i = 0; i < size; i++)
                {
                    var chain = Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_url_chain_at(_urlResponseInfoPtr, i);
                    chains.Add(chain);
                }
                return chains;
            }
            set
            {
                foreach (string chain in value)
                    Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_url_chain_add(_urlResponseInfoPtr, chain);
            }
        }

        /// <summary>
        /// Returns true if the response came from the cache, including requests that were revalidated over the network before being retrieved from the cache.
        /// </summary>
        /// <value></value>
        public bool WasCached
        {
            get => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_was_cached_get(_urlResponseInfoPtr);
            set => Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_was_cached_set(_urlResponseInfoPtr, value);
        }
    }
}
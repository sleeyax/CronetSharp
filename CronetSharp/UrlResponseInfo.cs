using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        /// <returns></returns>
        public IDictionary<string, IList<string>> GetAllHeaders()
        {
            IntPtr firstHeader =  Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_all_headers_list_at(_urlResponseInfoPtr, 0);
            // TODO: return actual headers
            // 
            return new Dictionary<string, IList<string>>();
        }

        /// <summary>
        /// Returns the HTTP status code.
        /// When a resource is retrieved from the cache, whether it was revalidated or not, the original status code is returned.
        /// </summary>
        /// <returns></returns>
        public int GetHttpStatusCode()
        {
            return Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_http_status_code_get(_urlResponseInfoPtr);
        }

        /// <summary>
        /// Returns the HTTP status text of the status line.
        /// For example, if the request received a "HTTP/1.1 200 OK" response, this method returns "OK".
        /// </summary>
        /// <returns></returns>
        public string GetHttpStatusCodeText()
        {
            return Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_http_status_text_get(_urlResponseInfoPtr);
        }

        /// <summary>
        /// Returns the protocol (for example 'quic/1+spdy/3') negotiated with the server.
        /// Returns an empty string if no protocol was negotiated, the protocol is not known, or when using plain HTTP or HTTPS.
        /// </summary>
        /// <returns></returns>
        public string GetNegotiatedProtocol()
        {
            return Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_negotiated_protocol_get(_urlResponseInfoPtr);
        }

        /// <summary>
        /// Returns the proxy server that was used for the request.
        /// </summary>
        /// <returns></returns>
        public string GetProxyServer()
        {
            return Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_proxy_server_get(_urlResponseInfoPtr);
        }

        /// <summary>
        /// Returns a minimum count of bytes received from the network to process this request.
        /// This count may ignore certain overheads (for example IP and TCP/UDP framing, SSL handshake and framing, proxy handling).
        /// This count is taken prior to decompression (for example GZIP) and includes headers and data from all redirects.
        /// This value may change (even for one UrlResponseInfo instance) as the request progresses until completion, when onSucceeded(), onFailed(), or onCanceled() is called.
        /// </summary>
        /// <returns></returns>
        public long GetReceivedByteCount()
        {
            return Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_received_byte_count_get(_urlResponseInfoPtr);
        }

        /// <summary>
        /// Returns the URL the response is for.
        /// This is the URL after following redirects, so it may not be the originally requested URL.
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_url_get(_urlResponseInfoPtr);
        }

        /// <summary>
        /// Returns the URL chain.
        /// The first entry is the originally requested URL; the following entries are redirects followed.
        /// </summary>
        /// <returns></returns>
        public IList<string> GetUrlChain()
        {
            string firstChain = Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_url_chain_at(_urlResponseInfoPtr, 0);
            // TODO: get whole chain (we can probably use .size() in a while/for loop to get every item in the chain)
            return new List<string> {firstChain};
        }

        /// <summary>
        /// Returns true if the response came from the cache, including requests that were revalidated over the network before being retrieved from the cache.
        /// </summary>
        /// <returns></returns>
        public bool WasCached()
        {
            return Cronet.UrlResponseInfo.Cronet_UrlResponseInfo_was_cached_get(_urlResponseInfoPtr);
        }
    }
}
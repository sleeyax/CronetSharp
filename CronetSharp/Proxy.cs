using System;
using System.Text;

namespace CronetSharp
{
    public class Proxy
    {
        /// <summary>
        /// Protocol that's used to connect to the proxy
        ///
        /// E.g. HTTP, HTTPS or SOCKS5
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// Host or ip address
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Port to connect to
        /// </summary>
        public int Port { get; set; }
        
        /// <summary>
        /// Username for basic authentication (optional)
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// Password for basic authentication (optional)
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Whether or not this proxy requires authentication
        /// </summary>
        public bool IsAuthenticated => Username != null && Password != null;

        public Proxy(string address, int port, string username, string password)
        {
            Protocol = "http";
            Address = address;
            Port = port;
            Username = username;
            Password = password;
        }
        
        public Proxy(string protocol, string address, int port, string username, string password) : this(address, port, username, password)
        {
            Protocol = protocol;
        }

        public Proxy(string address, int port)
        {
            Protocol = "http";
            Address = address;
            Port = port;
        }
        
        public Proxy(string protocol, string address, int port) : this(address, port)
        {
            Protocol = protocol;
        }

        /// <summary>
        /// Construct a new Proxy from a proxy string
        /// </summary>
        /// <param name="proxy"></param>
        /// <exception cref="ArgumentException"></exception>
        public Proxy(string proxy)
        {
            if (!proxy.Contains("://"))
                proxy = "http://" + proxy;

            ProxyFormat format = proxy.Contains("@") ? ProxyFormat.ReverseNotation : ProxyFormat.Normal;
            if (format == ProxyFormat.ReverseNotation)
                proxy = proxy.Replace("@", ":");

            var split = proxy.Split(new[]{"://"}, StringSplitOptions.None);
            Protocol = split[0];

            ParseProxy(split[1], format);
        }

        /// <summary>
        /// Tries to parse given proxy string into a Proxy object.
        /// Returns null on failure.
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static Proxy TryParse(string proxy)
        {
            try
            {
                return new Proxy(proxy);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Parse given proxy that is stripped from protocol and format specific characters
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="format"></param>
        /// <exception cref="ArgumentException"></exception>
        private void ParseProxy(string proxy, ProxyFormat format)
        {
            string[] proxyParts = proxy.Split(':');
            
            if (proxyParts.Length != 2 && proxyParts.Length != 4)
                throw new ArgumentException("Invalid proxy!");
            
            if (format == ProxyFormat.Normal)
            {
                Port = int.Parse(proxyParts[1]);
                Address = proxyParts[0];
            
                if (proxyParts.Length == 4)
                {
                    Username = proxyParts[2];
                    Password = proxyParts[3];
                }
            }
            else if (format == ProxyFormat.ReverseNotation)
            {
                if (proxyParts.Length == 4)
                {
                    Username = proxyParts[0];
                    Password = proxyParts[1];
                    Address = proxyParts[2];
                    Port = int.Parse(proxyParts[3]);
                }
                else
                {
                    Address = proxyParts[0];
                    Port = int.Parse(proxyParts[1]);
                }
            }
            else
            {
                throw new ArgumentException($"Unknown proxy format '{format}'!");
            }
        }
        
        public string EncodeBasic() => Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{Password}"));

        /// <summary>
        /// Stringify the proxy to a specific format 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string Format(ProxyFormat format)
        {
            if (format == ProxyFormat.ReverseNotation)
                return $"{(IsAuthenticated ? $"{Username}:{Password}@" : "")}{Address}:{Port}";
            
            return $"{Address}:{Port}{(IsAuthenticated ? $":{Username}:{Password}" : "")}";
        }

        public override string ToString() => Format(ProxyFormat.Normal);
    }
}
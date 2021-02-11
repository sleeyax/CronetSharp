using System;
using System.Collections.Generic;
using System.Text;

namespace example.Examples.Proxies
{
    public class Proxy
    {
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
            Address = address;
            Port = port;
            Username = username;
            Password = password;
        }

        public Proxy(string address, int port)
        {
            Address = address;
            Port = port;
        }

        /// <summary>
        /// Construct a new Proxy from a proxy string with a format of address:port[:username:password]
        /// </summary>
        /// <param name="proxy"></param>
        /// <exception cref="ArgumentException"></exception>
        public Proxy(string proxy)
        {
            string[] proxyParts = proxy.Split(':');
            
            if (proxyParts.Length != 2 && proxyParts.Length != 4)
                throw new ArgumentException("Invalid proxy string specified! Format is address:port[:username:password]");
            
            Port = int.Parse(proxyParts[1]);
            Address = proxyParts[0];
            
            if (proxyParts.Length == 4)
            {
                Username = proxyParts[2];
                Password = proxyParts[3];
            }
        }
        
        public string EncodeBasic() => Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{Password}"));
    }
}
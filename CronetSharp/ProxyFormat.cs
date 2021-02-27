namespace CronetSharp
{
    public enum ProxyFormat
    {
        /// <summary>
        /// Format: protocol://address:port[:username:password]
        /// </summary>
        Normal,
        /// <summary>
        /// Format: protocol://[username:password@]address:port
        /// </summary>
        ReverseNotation
    }
}
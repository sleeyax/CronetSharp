namespace CronetSharp.Cronet
{
    public enum ErrorCode
    {
        Callback,
        HostnameNotResolved,
        InternetDisconnected,
        NetworkChanged,
        TimedOut,
        ConnectionClosed,
        ConnectionTimedOut,
        ConnectionRefused,
        ConnectionReset,
        AddressUnreachable,
        QuicProtocolFailed,
        Other
    }
}
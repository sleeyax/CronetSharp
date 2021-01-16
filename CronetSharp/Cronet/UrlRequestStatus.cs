namespace CronetSharp.Cronet
{
    public enum UrlRequestStatus
    {
        Invalid = -1,
        Idle = 0,
        WaitingForStalledSocketPool,
        WaitingForAvailableSocket,
        WaitingForDelegate,
        WaitingForCache,
        DownloadingPacFile,
        ResolvingProxyForUrl,
        ResolvingHostInPacFile,
        EstablishingProxyTunnel,
        ResolvingHost,
        Connecting,
        SslHandshake,
        SendingRequest,
        WaitingForResponse,
        ReadingResponse,
    }
}
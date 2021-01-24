using System;

namespace CronetSharp
{
    public class QuicHint
    {
        public IntPtr Pointer { get; }
        
        public QuicHint()
        {
            Pointer = Cronet.QuicHint.Cronet_QuicHint_Create();
        }

        public QuicHint(IntPtr quicHintPointer)
        {
            Pointer = quicHintPointer;
        }

        public QuicHint(string host, int port, int alternatePort)
        {
            Pointer = Cronet.QuicHint.Cronet_QuicHint_Create();
            Host = host;
            Port = port;
            AlternatePort = alternatePort;
        }

        public void Destroy()
        {
            Cronet.QuicHint.Cronet_QuicHint_Destroy(Pointer);
        }

        /// <summary>
        /// Sets hostname or ip address.
        /// </summary>
        public string Host
        {
            get => Cronet.QuicHint.Cronet_QuicHint_host_get(Pointer);
            set => Cronet.QuicHint.Cronet_QuicHint_host_set(Pointer, value);
        }
        
        /// <summary>
        /// Sets port.
        /// </summary>
        public int Port
        {
            get => Cronet.QuicHint.Cronet_QuicHint_port_get(Pointer);
            set => Cronet.QuicHint.Cronet_QuicHint_port_set(Pointer, value);
        }
        
        /// <summary>
        /// Sets alternative port.
        /// </summary>
        public int AlternatePort
        {
            get => Cronet.QuicHint.Cronet_QuicHint_alternate_port_get(Pointer);
            set => Cronet.QuicHint.Cronet_QuicHint_alternate_port_set(Pointer, value);
        }
    }
}
using System;

namespace CronetSharp
{
    public class QuicHint
    {
        private readonly IntPtr _quicHintPtr;
        
        public QuicHint()
        {
            _quicHintPtr = Cronet.QuicHint.Cronet_QuicHint_Create();
        }

        public void Destroy()
        {
            Cronet.QuicHint.Cronet_QuicHint_Destroy(_quicHintPtr);
        }

        /// <summary>
        /// Sets hostname or ip address.
        /// </summary>
        public string Host
        {
            get => Cronet.QuicHint.Cronet_QuicHint_host_get(_quicHintPtr);
            set => Cronet.QuicHint.Cronet_QuicHint_host_set(_quicHintPtr, value);
        }
        
        /// <summary>
        /// Sets port.
        /// </summary>
        public int Port
        {
            get => Cronet.QuicHint.Cronet_QuicHint_port_get(_quicHintPtr);
            set => Cronet.QuicHint.Cronet_QuicHint_port_set(_quicHintPtr, value);
        }
        
        /// <summary>
        /// Sets alternative port.
        /// </summary>
        public int AlternatePort
        {
            get => Cronet.QuicHint.Cronet_QuicHint_alternate_port_get(_quicHintPtr);
            set => Cronet.QuicHint.Cronet_QuicHint_alternate_port_set(_quicHintPtr, value);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace CronetSharp
{
    public class PublicKeyPins
    {
        private readonly IntPtr _publicKeyPinsPtr;
        
        public PublicKeyPins()
        {
            _publicKeyPinsPtr = Cronet.PublicKeyPins.Cronet_PublicKeyPins_Create();
        }

        public void Destroy()
        {
            Cronet.PublicKeyPins.Cronet_PublicKeyPins_Destroy(_publicKeyPinsPtr);
        }

        public string Host
        {
            get => Cronet.PublicKeyPins.Cronet_PublicKeyPins_host_get(_publicKeyPinsPtr);
            set => Cronet.PublicKeyPins.Cronet_PublicKeyPins_host_set(_publicKeyPinsPtr, value);
        }

        public bool IncludeSubdomains
        {
            get => Cronet.PublicKeyPins.Cronet_PublicKeyPins_include_subdomains_get(_publicKeyPinsPtr);
            set => Cronet.PublicKeyPins.Cronet_PublicKeyPins_include_subdomains_set(_publicKeyPinsPtr, value);
        }

        public long ExpirationDate
        {
            get => Cronet.PublicKeyPins.Cronet_PublicKeyPins_expiration_date_get(_publicKeyPinsPtr);
            set => Cronet.PublicKeyPins.Cronet_PublicKeyPins_expiration_date_set(_publicKeyPinsPtr, value);
        }

        /// <summary>
        /// Set SHA256 pins.
        /// </summary>
        public IList<string> Pins
        {
            get
            { 
                var pins = new List<string>();
                var size = Cronet.PublicKeyPins.Cronet_PublicKeyPins_pins_sha256_size(_publicKeyPinsPtr);
                for (uint i = 0; i < size; i++)
                {
                    var pin = Cronet.PublicKeyPins.Cronet_PublicKeyPins_pins_sha256_at(_publicKeyPinsPtr, i);
                    pins.Add(pin);
                }
                return pins;
            }
            set
            {
                foreach (var pin in value)
                {
                    AddPin(pin);
                }
            }
            
        }
        
        /// <summary>
        /// Add a single SHA256 pin.
        /// </summary>
        /// <param name="sha256"></param>
        public void AddPin(string sha256)
        {
            Cronet.PublicKeyPins.Cronet_PublicKeyPins_pins_sha256_add(_publicKeyPinsPtr, sha256);
        }

        /// <summary>
        /// Clear all pins.
        /// </summary>
        public void ClearPins()
        {
            Cronet.PublicKeyPins.Cronet_PublicKeyPins_pins_sha256_clear(_publicKeyPinsPtr);
        }
    }
}
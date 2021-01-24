using System;
using System.Collections.Generic;

namespace CronetSharp
{
    public class PublicKeyPins : IDisposable
    {
        public IntPtr Pointer { get; }
        
        public PublicKeyPins()
        {
            Pointer = Cronet.PublicKeyPins.Cronet_PublicKeyPins_Create();
        }

        public PublicKeyPins(IntPtr publicKeyPinsPointer)
        {
            Pointer = publicKeyPinsPointer;
        }
        
        public PublicKeyPins(string hostname, string[] pinsSha256, bool includeSubdomains, DateTime expirationDate)
        {
            Pointer = Cronet.PublicKeyPins.Cronet_PublicKeyPins_Create();
            Host = hostname;
            Pins = pinsSha256;
            IncludeSubdomains = includeSubdomains;
            ExpirationDate = (long) (expirationDate - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public void Dispose()
        {
            Cronet.PublicKeyPins.Cronet_PublicKeyPins_Destroy(Pointer);
        }

        public string Host
        {
            get => Cronet.PublicKeyPins.Cronet_PublicKeyPins_host_get(Pointer);
            set => Cronet.PublicKeyPins.Cronet_PublicKeyPins_host_set(Pointer, value);
        }

        public bool IncludeSubdomains
        {
            get => Cronet.PublicKeyPins.Cronet_PublicKeyPins_include_subdomains_get(Pointer);
            set => Cronet.PublicKeyPins.Cronet_PublicKeyPins_include_subdomains_set(Pointer, value);
        }

        /// <summary>
        /// Set expiration date (number of milliseconds since epoch)
        /// </summary>
        public long ExpirationDate
        {
            get => Cronet.PublicKeyPins.Cronet_PublicKeyPins_expiration_date_get(Pointer);
            set => Cronet.PublicKeyPins.Cronet_PublicKeyPins_expiration_date_set(Pointer, value);
        }

        /// <summary>
        /// Set SHA256 pins.
        /// </summary>
        public string[] Pins
        {
            get
            { 
                var size = Cronet.PublicKeyPins.Cronet_PublicKeyPins_pins_sha256_size(Pointer);
                var pins = new string[size];
                for (uint i = 0; i < size; i++)
                {
                    var pin = Cronet.PublicKeyPins.Cronet_PublicKeyPins_pins_sha256_at(Pointer, i);
                    pins[i] = pin;
                }
                return pins;
            }
            set
            {
                foreach (var pin in value)
                    AddPin(pin);
            }
            
        }
        
        /// <summary>
        /// Add a single SHA256 pin.
        /// </summary>
        /// <param name="sha256"></param>
        public void AddPin(string sha256)
        {
            Cronet.PublicKeyPins.Cronet_PublicKeyPins_pins_sha256_add(Pointer, sha256);
        }

        /// <summary>
        /// Clear all pins.
        /// </summary>
        public void ClearPins()
        {
            Cronet.PublicKeyPins.Cronet_PublicKeyPins_pins_sha256_clear(Pointer);
        }
    }
}
using System;

namespace CronetSharp
{
    public class HttpHeader
    {
        public IntPtr Pointer { get; }

        public HttpHeader(IntPtr httpHeaderPtr)
        {
            Pointer = httpHeaderPtr;
        }
        
        public HttpHeader()
        {
            Pointer = Cronet.HttpHeader.Cronet_HttpHeader_Create();
        }
        
        public HttpHeader(string name, string value)
        {
            Pointer = Cronet.HttpHeader.Cronet_HttpHeader_Create();
            Name = name;
            Value = value;
        }

        public void Destroy()
        {
            Cronet.HttpHeader.Cronet_HttpHeader_Destroy(Pointer);
        }

        public string Name
        {
            get => Cronet.HttpHeader.Cronet_HttpHeader_name_get(Pointer);
            set => Cronet.HttpHeader.Cronet_HttpHeader_name_set(Pointer, value);
        }
        
        public string Value
        {
            get => Cronet.HttpHeader.Cronet_HttpHeader_value_get(Pointer);
            set => Cronet.HttpHeader.Cronet_HttpHeader_value_set(Pointer, value);
        }
    }
}
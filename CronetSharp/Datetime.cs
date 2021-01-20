using System;

namespace CronetSharp
{
    public class Datetime
    {
        private readonly IntPtr _dateTimePtr;

        public Datetime()
        {
            _dateTimePtr = Cronet.DateTime.Cronet_DateTime_Create();
        }

        public Datetime(long value)
        {
            _dateTimePtr = Cronet.DateTime.Cronet_DateTime_Create();
            Value = value;
        }

        public void Destroy()
        {
            Cronet.DateTime.Cronet_DateTime_Destroy(_dateTimePtr);
        }

        public long Value
        {
            get => Cronet.DateTime.Cronet_DateTime_value_get(_dateTimePtr);
            set => Cronet.DateTime.Cronet_DateTime_value_set(_dateTimePtr, value);
        }
    }
}
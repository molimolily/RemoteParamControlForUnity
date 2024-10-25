using System;

namespace RPC
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RemoteControllableAttribute : Attribute
    {
        public string Address { get; }
        public RemoteControllableAttribute()
        {
        }

        public RemoteControllableAttribute(string address)
        {
            Address = address;
        }
    }
}

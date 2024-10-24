using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

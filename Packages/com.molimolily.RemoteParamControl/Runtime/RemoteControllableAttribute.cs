using System;

namespace RPC
{
    /// <summary>
    /// Attribute to mark fields as remote controllable via OSC.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RemoteControllableAttribute : Attribute
    {
        /// <summary>
        /// Gets the OSC address associated with the field.
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteControllableAttribute"/> class.
        /// </summary>
        public RemoteControllableAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteControllableAttribute"/> class with a specified address.
        /// </summary>
        /// <param name="address">The OSC address associated with the field.</param>
        public RemoteControllableAttribute(string address)
        {
            Address = address;
        }
    }
}

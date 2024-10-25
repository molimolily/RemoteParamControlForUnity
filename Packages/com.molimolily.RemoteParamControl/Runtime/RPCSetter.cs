using OscJack;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RPC
{
    /// <summary>
    /// RPCSetter is an abstract base class for setting up remote parameter control via OSC.
    /// </summary>
    public abstract class RPCSetter : MonoBehaviour
    {
        [SerializeField] OscConnection oscConnection;

        protected OscServer oscServer;
        private Dictionary<string, Type> addressParameterTypes;

        private Queue<Action> mainThreadActions = new Queue<Action>();

        protected virtual void OnEnable()
        {
#if UNITY_STANDALONE
            oscServer = new OscServer(oscConnection.port);
            addressParameterTypes = GetAddressParameterTypes();
            SetCallbacks();
#endif
        }

        protected virtual void OnDisable()
        {
#if UNITY_STANDALONE
            oscServer?.Dispose();
            oscServer = null;
#endif
        }

        /// <summary>
        /// Abstract method to set up callbacks for OSC messages.
        /// </summary>
        protected abstract void SetCallbacks();

        /// <summary>
        /// Retrieves the parameter types for each address from the fields marked with RemoteControllableAttribute.
        /// </summary>
        /// <returns>A dictionary mapping addresses to their parameter types.</returns>
        private Dictionary<string, Type> GetAddressParameterTypes()
        {
            var addressParameterTypes = new Dictionary<string, Type>();
            var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var attribute = (RemoteControllableAttribute)Attribute.GetCustomAttribute(field, typeof(RemoteControllableAttribute));
                if (attribute != null)
                {
                    addressParameterTypes[attribute.Address] = field.FieldType;
                }
            }
            return addressParameterTypes;
        }

        /// <summary>
        /// Converts OSC data to the appropriate type based on the address.
        /// </summary>
        /// <param name="address">The OSC address.</param>
        /// <param name="data">The OSC data handle.</param>
        /// <returns>The converted value.</returns>
        protected object GetValueFromOscData(string address, OscDataHandle data)
        {
            if (!addressParameterTypes.ContainsKey(address))
            {
                throw new ArgumentException($"Address {address} not found.");
            }

            var type = addressParameterTypes[address];
            if (type == typeof(int))
            {
                return data.GetElementAsInt(0);
            }
            else if (type == typeof(float))
            {
                return data.GetElementAsFloat(0);
            }
            else if (type == typeof(Vector2))
            {
                return new Vector2(data.GetElementAsFloat(0), data.GetElementAsFloat(1));
            }
            else if (type == typeof(Vector2Int))
            {
                return new Vector2Int(data.GetElementAsInt(0), data.GetElementAsInt(1));
            }
            else if (type == typeof(Vector3))
            {
                return new Vector3(data.GetElementAsFloat(0), data.GetElementAsFloat(1), data.GetElementAsFloat(2));
            }
            else if (type == typeof(Vector3Int))
            {
                return new Vector3Int(data.GetElementAsInt(0), data.GetElementAsInt(1), data.GetElementAsInt(2));
            }
            else if (type == typeof(Vector4))
            {
                return new Vector4(data.GetElementAsFloat(0), data.GetElementAsFloat(1), data.GetElementAsFloat(2), data.GetElementAsFloat(3));
            }
            else if (type == typeof(Color))
            {
                return new Color(data.GetElementAsFloat(0), data.GetElementAsFloat(1), data.GetElementAsFloat(2), data.GetElementAsFloat(3));
            }
            else if (type == typeof(Rect))
            {
                return new Rect(data.GetElementAsFloat(0), data.GetElementAsFloat(1), data.GetElementAsFloat(2), data.GetElementAsFloat(3));
            }
            else if (type == typeof(RectInt))
            {
                return new RectInt(data.GetElementAsInt(0), data.GetElementAsInt(1), data.GetElementAsInt(2), data.GetElementAsInt(3));
            }
            else if (type == typeof(bool))
            {
                return data.GetElementAsInt(0) != 0;
            }
            else if (type == typeof(string))
            {
                return data.GetElementAsString(0);
            }
            else
            {
                throw new NotSupportedException($"Type {type} is not supported.");
            }
        }

        /// <summary>
        /// Adds a callback for a specific OSC address.
        /// </summary>
        /// <param name="address">The OSC address.</param>
        /// <param name="callback">The callback to be invoked when a message is received.</param>
        protected void AddCallback(string address, Action<object> callback)
        {
            oscServer.MessageDispatcher.AddCallback(
                address,
                (string addr, OscDataHandle data) =>
                {
                    var value = GetValueFromOscData(addr, data);
                    callback(value);
                }
            );
        }

        /// <summary>
        /// Enqueues an action to be executed on the main thread.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        protected void EnqueueMainThreadAction(Action action)
        {
            lock (mainThreadActions)
            {
                mainThreadActions.Enqueue(action);
            }
        }

        protected void Update()
        {
            lock (mainThreadActions)
            {
                while (mainThreadActions.Count > 0)
                {
                    mainThreadActions.Dequeue().Invoke();
                }
            }
        }
    }
}

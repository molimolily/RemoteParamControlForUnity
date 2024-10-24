using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPC
{
    [Serializable]
    public abstract class RPCParamBase
    {
        public string name;
        public RPCLayoutType layoutType;
        public string address;

        public float min;
        public float max;

        public RPCParamBase(string name, RPCLayoutType layoutType, string address)
        {
            this.name = name;
            this.layoutType = layoutType;
            this.address = address;
        }

        public abstract object GetValue();
        public abstract void SetValue(object value);
    }


    [Serializable]
    public class RPCParamInt : RPCParamBase
    {
        [SerializeField] public int value;

        public RPCParamInt(string name, RPCLayoutType layoutType, int value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            this.value = Convert.ToInt32(value);
        }
    }

    [Serializable]
    public class RPCParamFloat : RPCParamBase
    {
        [SerializeField] public float value;

        public RPCParamFloat(string name, RPCLayoutType layoutType, float value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            this.value = Convert.ToSingle(value);
        }
    }

    [Serializable]
    public class RPCParamVector2 : RPCParamBase
    {
        [SerializeField] public Vector2 value;

        public RPCParamVector2(string name, RPCLayoutType layoutType, Vector2 value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is Vector2 vec)
            {
                this.value = vec;
            }
            else
            {
                Debug.LogError("Invalid value type for Vector2");
            }
        }
    }

    [Serializable]
    public class RPCParamString : RPCParamBase
    {
         [SerializeField] public string value;

        public RPCParamString(string name, RPCLayoutType layoutType, string value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            this.value = value as string;
        }
    }

}
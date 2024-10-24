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

    public enum RPCLayoutType
    {
        Slider,
        IntSlider,
        Toggle,
        FloatField,
        IntField,
        Vector2Field,
        Vector2IntField,
        Vector3Field,
        Vector3IntField,
        Vector4Field,
        ColorField,
        RectField,
        RectIntField,
        TextField,
        EnumPopup
    }

    [System.Serializable]
    public class RPCParam
    {
        public string name;
        public RPCLayoutType layoutType;
        public Type type;
        public object value;
        public string address;

        public RPCParam(string name, RPCLayoutType layoutType, Type type, object value, string address)
        {
            this.name = name;
            this.layoutType = layoutType;
            this.type = type;
            this.value = value;
            this.address = address;
        }

        public override string ToString() 
        {
            return name + ": " + value.ToString();
        }
    }

    [CreateAssetMenu(fileName = "RPCData", menuName = "ScriptableObjects/RPC/RPCData", order = 1)]
    public class RPCData : ScriptableObject
    {
        public int testInt;
        public List<RPCParam> parameters = new List<RPCParam>();

        public RPCData()
        {
            parameters.Add(new RPCParam("testInt", RPCLayoutType.IntField, typeof(int), testInt, "testInt"));
        }
    }
}
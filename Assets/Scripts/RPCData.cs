using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPC
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RemoteControllableAttribute : Attribute
    {
        
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
    public class RPCParam
    {
        public string name;
        public RPCLayoutType layoutType;
        public Type type;
        public object value;

        public RPCParam(string name, RPCLayoutType layoutType, Type type, object value)
        {
            this.name = name;
            this.layoutType = layoutType;
            this.type = type;
            this.value = value;
        }

        public override string ToString() 
        {
            return name + ": " + value.ToString();
        }
    }

    [CreateAssetMenu(fileName = "RPCData", menuName = "ScriptableObjects/RPC/RPCData", order = 1)]
    public class RPCData : ScriptableObject
    {
        public List<RPCParam> parameters = new List<RPCParam>();
    }
}
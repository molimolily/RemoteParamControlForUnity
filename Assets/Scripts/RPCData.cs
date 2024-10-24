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
        TextField
    }

    [CreateAssetMenu(fileName = "RPCData", menuName = "ScriptableObjects/RPC/RPCData", order = 1)]
    public class RPCData : ScriptableObject
    {
        [SerializeReference, HideInInspector] public List<RPCParamBase> parameters = new List<RPCParamBase>();
    }
}
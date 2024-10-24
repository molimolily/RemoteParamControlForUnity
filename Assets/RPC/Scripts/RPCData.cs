using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPC
{
    [CreateAssetMenu(fileName = "RPCData", menuName = "ScriptableObjects/RPC/RPCData", order = 1)]
    public class RPCData : ScriptableObject
    {
        [SerializeReference, HideInInspector] public List<RPCParamBase> parameters = new List<RPCParamBase>();
    }
}
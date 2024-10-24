using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPC;

public class SetterSample : RPCSetter
{
    [SerializeField] RPCSample rpcSample;

    [Header("Parameters")]
    [RemoteControllable("value0")] public int intVal = 0;
    [RemoteControllable, Range(0.0f, 1.0f)] public float floatVal = 1.0f;
    [RemoteControllable("Sample/Data")] public Vector2 vector2Val = new Vector2(1.0f, 1.0f);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPC;

public class SetterSample : RPCSetter
{
    [SerializeField] RPCSample rpcSample;

    [Header("Parameters")]
    [RemoteControllable] public int intVal = 0;
    [RemoteControllable] public float floatVal = 1.0f;
    [RemoteControllable] public Vector2 vector2Val = new Vector2(1.0f, 1.0f);

}

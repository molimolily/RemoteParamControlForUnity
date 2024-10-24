using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPC;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using OscJack;

public class SetterSample : RPCSetter
{
    [SerializeField] RPCSample rpcSample;

    const string intValAdr = "value/i";
    const string floatValAdr = "value/f";
    const string sliderValAdr = "Slider/f";
    const string intSliderValAdr = "Slider/i";
    const string vector2ValAdr = "vec2/f";
    const string vector2IntValAdr = "vec2/i";
    const string vector3ValAdr = "vec3/f";
    const string vector3IntValAdr = "vec3/i";
    const string vector4ValAdr = "vec4/f";
    const string colorValAdr = "color";
    const string rectValAdr = "rect/f";
    const string rectIntValAdr = "rect/i";
    const string boolValAdr = "bool";
    const string stringValAdr = "text";

    [Header("Parameters")]
    [RemoteControllable(intValAdr)] public int intVal = 0;
    [RemoteControllable(floatValAdr)] public float floatVal = 0.0f;
    [RemoteControllable(sliderValAdr), Range(0.0f, 1.0f)] public float sliderVal = 0.0f;
    [RemoteControllable(intSliderValAdr), Range(0, 10)] public int intSliderVal = 0;
    [RemoteControllable(vector2ValAdr)] public Vector2 vector2Val = new Vector2(1.0f, 1.0f);
    [RemoteControllable(vector2IntValAdr)] public Vector2Int vector2IntVal = new Vector2Int(1, 1);
    [RemoteControllable(vector3ValAdr)] public Vector3 vector3Val = new Vector3(1.0f, 1.0f, 1.0f);
    [RemoteControllable(vector3IntValAdr)] public Vector3Int vector3IntVal = new Vector3Int(1, 1, 1);
    [RemoteControllable(vector4ValAdr)] public Vector4 vector4Val = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    [RemoteControllable(colorValAdr)] public Color colorVal = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [RemoteControllable(rectValAdr)] public Rect rectVal = new Rect(1.0f, 1.0f, 1.0f, 1.0f);
    [RemoteControllable(rectIntValAdr)] public RectInt rectIntVal = new RectInt(1, 1, 1, 1);
    [RemoteControllable(boolValAdr)] public bool boolVal = false;
    [RemoteControllable(stringValAdr)] public string stringVal = "Hello, World!";

    protected override void SetCallbacks()
    {
        AddCallback(intValAdr, value => rpcSample.intVal = (int)value);
        AddCallback(floatValAdr, value => rpcSample.floatVal = (float)value);
        AddCallback(sliderValAdr, value => rpcSample.sliderVal = (float)value);
        AddCallback(intSliderValAdr, value => rpcSample.intSliderVal = (int)value);
        AddCallback(vector2ValAdr, value => rpcSample.vector2Val = (Vector2)value);
        AddCallback(vector2IntValAdr, value => rpcSample.vector2IntVal = (Vector2Int)value);
        AddCallback(vector3ValAdr, value => rpcSample.vector3Val = (Vector3)value);
        AddCallback(vector3IntValAdr, value => rpcSample.vector3IntVal = (Vector3Int)value);
        AddCallback(vector4ValAdr, value => rpcSample.vector4Val = (Vector4)value);
        AddCallback(colorValAdr, value => rpcSample.colorVal = (Color)value);
        AddCallback(rectValAdr, value => rpcSample.rectVal = (Rect)value);
        AddCallback(rectIntValAdr, value => rpcSample.rectIntVal = (RectInt)value);
        AddCallback(boolValAdr, value => rpcSample.boolVal = (bool)value);
        AddCallback(stringValAdr, value => rpcSample.stringVal = (string)value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

namespace RPC
{
    public class RPCGenerator : EditorWindow
    {

        [SerializeField] private RPCLayoutObjectField<RPCSetter> targetField;

        [SerializeField] private RPCLayoutObjectField<RPCData> rpcDataField;

        private Vector2 scrollPos;

        [MenuItem("Window/RPC/RPC Generator")]
        public static void ShowWindow()
        {
            GetWindow<RPCGenerator>().Show();
        }

        private void Awake()
        {
            targetField = new RPCLayoutObjectField<RPCSetter>(new GUIContent("Target"), null);
            rpcDataField = new RPCLayoutObjectField<RPCData>(new GUIContent("RPC Data"), null);
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            targetField.Draw();
            rpcDataField.Draw();

            EditorGUILayout.Space();

            EditorGUI.BeginDisabledGroup(targetField.Value == null || rpcDataField.Value == null);

            if (GUILayout.Button("Generate RPC Data", GUILayout.Height(30.0f)))
            {
                GenerateRPCData();
            }

            EditorGUI.EndDisabledGroup();

            GUILayout.EndScrollView();
        }

        private void GenerateRPCData()
        {
            FieldInfo[] fields = targetField.Value.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            List<RPCParamBase> rpcParameters = new List<RPCParamBase>();
            foreach (FieldInfo field in fields)
            {
                if (field.GetCustomAttribute(typeof(RPC.RemoteControllableAttribute)) != null)
                {
                    string name = field.Name;
                    object value = field.GetValue(targetField.Value);
                    Type type = field.FieldType;
                    string address = field.GetCustomAttribute<RemoteControllableAttribute>().Address ?? name;
                    RangeAttribute rangeAttribute = (RangeAttribute)Attribute.GetCustomAttribute(field, typeof(RangeAttribute));
                    RPCLayoutType layoutType = 0;
                    RPCParamBase param = null;
                    switch (type)
                    {
                        case Type t when t == typeof(float):
                            if (rangeAttribute == null)
                            {
                                layoutType = RPCLayoutType.FloatField;
                                param = new RPCParamFloat(name, layoutType, (float)value, address);
                            }
                            else
                            {
                                layoutType = RPCLayoutType.Slider;
                                param = new RPCParamFloat(name, layoutType, (float)value, address);
                                param.min = rangeAttribute.min;
                                param.max = rangeAttribute.max;
                            }
                            break;
                        case Type t when t == typeof(int):
                            if(rangeAttribute == null)
                            {
                                layoutType = RPCLayoutType.IntField;
                                param = new RPCParamInt(name, layoutType, (int)value, address);
                            }
                            else
                            {
                                layoutType = RPCLayoutType.IntSlider;
                                param = new RPCParamInt(name, layoutType, (int)value, address);
                                param.min = rangeAttribute.min;
                                param.max = rangeAttribute.max;
                            }
                            break;
                        case Type t when t == typeof(Vector2):
                            layoutType = RPCLayoutType.Vector2Field;
                            param = new RPCParamVector2(name, layoutType, (Vector2)value, address);
                            break;
                        case Type t when t == typeof(Vector2Int):
                            layoutType = RPCLayoutType.Vector2IntField;
                            param = new RPCParamVector2Int(name, layoutType, (Vector2Int)value, address);
                            break;
                        case Type t when t == typeof(Vector3):
                            layoutType = RPCLayoutType.Vector3Field;
                            param = new RPCParamVector3(name, layoutType, (Vector3)value, address);
                            break;
                        case Type t when t == typeof(Vector3Int):
                            layoutType = RPCLayoutType.Vector3IntField;
                            param = new RPCParamVector3Int(name, layoutType, (Vector3Int)value, address);
                            break;
                        case Type t when t == typeof(Vector4):
                            layoutType = RPCLayoutType.Vector4Field;
                            param = new RPCParamVector4(name, layoutType, (Vector4)value, address);
                            break;
                        case Type t when t == typeof(Color):
                            layoutType = RPCLayoutType.ColorField;
                            param = new RPCParamColor(name, layoutType, (Color)value, address);
                            break;
                        case Type t when t == typeof(Rect):
                            layoutType = RPCLayoutType.RectField;
                            param = new RPCParamRect(name, layoutType, (Rect)value, address);
                            break;
                        case Type t when t == typeof(RectInt):
                            layoutType = RPCLayoutType.RectIntField;
                            param = new RPCParamRectInt(name, layoutType, (RectInt)value, address);
                            break;
                        case Type t when t == typeof(bool):
                            layoutType = RPCLayoutType.Toggle;
                            param = new RPCParamBool(name, layoutType, (bool)value, address);
                            break;
                        case Type t when t == typeof(string):
                            layoutType = RPCLayoutType.TextField;
                            param = new RPCParamString(name, layoutType, (string)value, address);
                            break;
                    }

                    if (param != null)
                        rpcParameters.Add(param);
                }
            }
            rpcDataField.Value.parameters = rpcParameters;
            EditorUtility.SetDirty(rpcDataField.Value);
            AssetDatabase.SaveAssets();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using Codice.CM.SEIDInfo;

namespace RPC
{
    public class RPCGenerator : EditorWindow
    {

        private RPCLayoutObjectField<RPCSetter> targetFiled;
        private RPCLayoutObjectField<RPCData> rpcDataFiled;

        private Vector2 scrollPos;

        [MenuItem("Window/RPC/RPC Generator")]
        public static void ShowWindow()
        {
            GetWindow<RPCGenerator>().Show();
        }

        private void OnEnable()
        {
            targetFiled = new RPCLayoutObjectField<RPCSetter>("Target", null);
            rpcDataFiled = new RPCLayoutObjectField<RPCData>("RPCData", null);
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            targetFiled.Draw();
            rpcDataFiled.Draw();

            EditorGUILayout.Space();

            EditorGUI.BeginDisabledGroup(targetFiled.Value == null || rpcDataFiled.Value == null);

            if (GUILayout.Button("Generate RPC Data", GUILayout.Height(30.0f)))
            {
                GenerateRPCData();
            }

            EditorGUI.EndDisabledGroup();

            GUILayout.EndScrollView();
        }

        private void GenerateRPCData()
        {
            FieldInfo[] fields = targetFiled.Value.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            List<RPCParam> rpcParameters = new List<RPCParam>();
            foreach (FieldInfo field in fields)
            {
                if (field.GetCustomAttribute(typeof(RPC.RemoteControllableAttribute)) != null)
                {
                    // Debug.Log($"Controllable Field: {field.Name}, Current Value: {field.GetValue(targetFiled.Value)}");
                    string name = field.Name;
                    Type type = field.FieldType;
                    object value = field.GetValue(targetFiled.Value);

                    RPCLayoutType layoutType = RPCLayoutType.IntField;
                    switch(type)
                    {
                        case Type t when t == typeof(int):
                            layoutType = RPCLayoutType.IntField;
                            break;
                        case Type t when t == typeof(float):
                            layoutType = RPCLayoutType.FloatField;
                            break;
                        case Type t when t == typeof(Vector2):
                            layoutType = RPCLayoutType.Vector2Field;
                            break;
                    }

                    Debug.Log($"Name: {name}, Type: {type}, Value: {value}, LayoutType: {layoutType.ToString()}");
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml.Linq;

namespace RPC
{
    [CustomEditor(typeof(RPCData))]
    public class RPCDataInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            
            RPCData rpcData = (RPCData)target;

            EditorGUILayout.LabelField("RPC Parameters", EditorStyles.boldLabel);

            foreach (RPCParam param in rpcData.parameters)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Name", param.name);
                EditorGUILayout.LabelField("Layout Type", param.layoutType.ToString());
                EditorGUILayout.LabelField("Type", param.type?.ToString());
                EditorGUILayout.LabelField("Value", param.value != null ? param.value.ToString() : "null");
                EditorGUILayout.LabelField("Address", param.address);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
    }
}
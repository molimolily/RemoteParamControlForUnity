using UnityEditor;

namespace RPC
{
    [CustomEditor(typeof(RPCData))]
    public class RPCDataInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            RPCData rpcData = (RPCData)target;

            EditorGUILayout.LabelField("RPC Parameters", EditorStyles.boldLabel);

            foreach (RPCParamBase param in rpcData.parameters)
            {
                if(param == null)
                    continue;
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Name", param.name);
                EditorGUILayout.LabelField("Layout Type", param.layoutType.ToString());
                EditorGUILayout.LabelField("Value", param.GetValue() != null ? param.GetValue().ToString() : "null");
                EditorGUILayout.LabelField("Address", param.address);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
    }
}
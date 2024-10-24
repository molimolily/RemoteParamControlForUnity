using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OscJack;

namespace RPC
{
    public class RPCWindow : EditorWindow
    {
        private OscClient client;

        private bool isStarted = false;
        private bool paramFoldState = true;
        private bool networkFoldState = true;
        private Vector2 scrollPos;

        [SerializeField] private RPCLayoutObjectField<OscConnection> rpcOscConnection;
        [SerializeField] private RPCLayoutObjectField<RPCData> rpcData;
        private List<RPCEditorLayoutBase> rpcLayouts;

        [MenuItem("Window/RPC/RPC Window")]
        public static void ShowWindow()
        {
            GetWindow<RPCWindow>("RPC Window");
        }

        public void Awake()
        {
            InitParams();
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            if(isStarted)
            {
                RPCDataLayout();
                NetworkLayout();

                EditorGUILayout.Space();

                // Stop OSC Client
                if (GUILayout.Button("Stop OSC Client", GUILayout.Height(30.0f)))
                {
                    StopCliant();
                }

                EditorGUILayout.Space();

                // Parameters
                paramFoldState = EditorGUILayout.BeginFoldoutHeaderGroup(paramFoldState, "Parametes");

                if(paramFoldState)
                {
                    DrawParamLayouts();
                }

                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            else
            {
                RPCDataLayout();
                NetworkLayout();

                EditorGUILayout.Space();

                EditorGUI.BeginDisabledGroup(rpcData.Value == null || rpcOscConnection.Value == null);
                if (GUILayout.Button("Start", GUILayout.Height(40.0f)))
                {
                    StartCliant();
                    GenerateEditorLayout();
                }
                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.Space();

            EditorGUILayout.EndScrollView();
        }

        public void OnDisable()
        {
            StopCliant();
        }

        private void InitParams()
        {
            rpcOscConnection = new RPCLayoutObjectField<OscConnection>("OSC Connection", null);
            rpcData = new RPCLayoutObjectField<RPCData>("RPC Data", null);
        }

        private void GenerateEditorLayout()
        {
            if(rpcData.Value == null)
            {
                return;
            }

            rpcLayouts = new List<RPCEditorLayoutBase>();

            foreach(RPCParamBase rpcParam in rpcData.Value.parameters)
            {
                RPCEditorLayoutBase layout = null;
                switch (rpcParam.layoutType)
                {
                    case RPCLayoutType.FloatField:
                        layout = new RPCFloatField(rpcParam.name, (float)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            client.Send(rpcParam.address, (float)value);
                        };
                        break;
                    case RPCLayoutType.IntField:
                        layout = new RPCIntField(rpcParam.name, (int)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            client.Send(rpcParam.address, (int)value);
                        };
                        break;
                    case RPCLayoutType.Slider:
                        layout = new RPCSlider(rpcParam.name, (float)rpcParam.GetValue(), rpcParam.min, rpcParam.max);
                        layout.OnValueChanged += (value) =>
                        {
                            client.Send(rpcParam.address, (float)value);
                        };
                        break;
                    case RPCLayoutType.IntSlider:
                        layout = new RPCIntSlider(rpcParam.name, (int)rpcParam.GetValue(), (int)rpcParam.min, (int)rpcParam.max);
                        layout.OnValueChanged += (value) =>
                        {
                            client.Send(rpcParam.address, (int)value);
                        };
                        break;
                    case RPCLayoutType.Vector2Field:
                        layout = new RPCVector2Field(rpcParam.name, (Vector2)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            Vector2 v = (Vector2)value;
                            client.Send(rpcParam.address, v.x, v.y);
                        };
                        break;
                    case RPCLayoutType.Vector2IntField:
                        layout = new RPCVector2IntField(rpcParam.name, (Vector2Int)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            Vector2Int v = (Vector2Int)value;
                            client.Send(rpcParam.address, v.x, v.y);
                        };
                        break;
                    case RPCLayoutType.Vector3Field:
                        layout = new RPCVector3Field(rpcParam.name, (Vector3)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            Vector3 v = (Vector3)value;
                            client.Send(rpcParam.address, v.x, v.y, v.z);
                        };
                        break;
                    case RPCLayoutType.Vector3IntField:
                        layout = new RPCVector3IntField(rpcParam.name, (Vector3Int)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            Vector3Int v = (Vector3Int)value;
                            client.Send(rpcParam.address, v.x, v.y, v.z);
                        };
                        break;
                    case RPCLayoutType.Vector4Field:
                        layout = new RPCVector4Field(rpcParam.name, (Vector4)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            Vector4 v = (Vector4)value;
                            client.Send(rpcParam.address, v.x, v.y, v.z, v.w);
                        };
                        break;
                    case RPCLayoutType.ColorField:
                        layout = new RPCColorField(rpcParam.name, (Color)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            Color c = (Color)value;
                            client.Send(rpcParam.address, c.r, c.g, c.b, c.a);
                        };
                        break;
                    case RPCLayoutType.RectField:
                        layout = new RPCRectField(rpcParam.name, (Rect)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            Rect r = (Rect)value;
                            client.Send(rpcParam.address, r.x, r.y, r.width, r.height);
                        };
                        break;
                    case RPCLayoutType.RectIntField:
                        layout = new RPCRectIntField(rpcParam.name, (RectInt)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            RectInt r = (RectInt)value;
                            client.Send(rpcParam.address, r.x, r.y, r.width, r.height);
                        };
                        break;
                    case RPCLayoutType.Toggle:
                        layout = new RPCToggleField(rpcParam.name, (bool)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            client.Send(rpcParam.address, (bool)value ? 1 : 0);
                        };
                        break;
                    case RPCLayoutType.TextField:
                        layout = new RPCTextField(rpcParam.name, (string)rpcParam.GetValue());
                        layout.OnValueChanged += (value) =>
                        {
                            client.Send(rpcParam.address, (string)value);
                        };
                        break;
                }
                
                if(layout != null)
                {
                    rpcLayouts.Add(layout);
                }

            }
        }

        private void DrawParamLayouts()
        {
            foreach (RPCEditorLayoutBase layout in rpcLayouts)
            {
                layout.Draw();
                EditorGUILayout.Space();
            }
        }

        private void StartCliant()
        {
            OscConnection oscConnection = rpcOscConnection.Value;
            if(oscConnection == null)
            {
                Debug.LogError("OSC Connection is null");
                return;
            }
            client = new OscClient(oscConnection.host, oscConnection.port);
            isStarted = true;
        }

        private void StopCliant()
        {
            client?.Dispose();
            client = null;
            isStarted = false;
        }

        private void NetworkLayout()
        {
            EditorGUILayout.LabelField("Network", EditorStyles.boldLabel);

            EditorGUI.BeginDisabledGroup(isStarted);
            rpcOscConnection.Draw();
            EditorGUI.EndDisabledGroup();

            if (isStarted)
            {
                networkFoldState = EditorGUILayout.BeginFoldoutHeaderGroup(networkFoldState, "Settings");
                if (networkFoldState)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.TextField("Host", rpcOscConnection.Value.host);
                    EditorGUILayout.IntField("Port", rpcOscConnection.Value.port);
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }

        private void RPCDataLayout()
        {
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(isStarted);
            rpcData.Draw();
            EditorGUI.EndDisabledGroup();
        }
    }
}

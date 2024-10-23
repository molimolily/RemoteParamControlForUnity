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

        private RPCLayoutObjectField<OscConnection> rpcOscConnection;
        private RPCSlider rpcSlider;
        private RPCIntSlider rpcIntSlider;
        private RPCFloat rpcFloat;
        private RPCEnumPopup<RPCLayoutType> rpcPopup;

        [MenuItem("Window/RPC/RPC Window")]
        public static void ShowWindow()
        {
            GetWindow<RPCWindow>("RPC Window");
        }

        public void OnEnable()
        {
            Debug.Log("RPC Window enabled");

            InitParams();
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            if(isStarted)
            {
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
                    using(var change = new EditorGUI.ChangeCheckScope())
                    {
                        rpcSlider.Draw();
                        rpcIntSlider.Draw();
                        rpcFloat.Draw();
                        rpcPopup.Draw();

                        if(change.changed)
                        {
                            Debug.Log("Changed");
                        }
                    }

                    if (GUILayout.Button("Send OSC"))
                    {
                        client.Send("/slider", rpcSlider.Value);
                    }
                }

                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            else
            {
                NetworkLayout();   
                if (rpcOscConnection.Value != null)
                {
                    if (GUILayout.Button("Start", GUILayout.Height(40.0f)))
                    {
                        StartCliant();
                    }
                }
                else
                {
                    GUILayout.Label("Assign RPC Manager to start OSC Client");
                }
            }

            EditorGUILayout.EndScrollView();
        }

        public void OnDisable()
        {
            Debug.Log("RPC Window disabled");
            StopCliant();
        }

        private void InitParams()
        {
            rpcOscConnection = new RPCLayoutObjectField<OscConnection>("OSC Connection", null);

            rpcSlider = new RPCSlider("Slider", 0.5f, 0.0f, 1.0f);
            rpcSlider.OnValueChanged += (newValue) => Debug.Log("Slider value changed to " + newValue);

            rpcIntSlider = new RPCIntSlider("Int Slider", 5, 0, 10);
            rpcIntSlider.OnValueChanged += (newValue) => Debug.Log("IntSlider value changed to " + newValue);

            rpcFloat = new RPCFloat("Float", 0.0f);
            rpcFloat.OnValueChanged += (newValue) => Debug.Log("Float value changed to " + newValue);

            rpcPopup = new RPCEnumPopup<RPCLayoutType>("Layout Type", RPCLayoutType.Slider);
            rpcPopup.OnValueChanged += (newValue) => Debug.Log("EnumPopup value changed to " + newValue);
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
            networkFoldState = EditorGUILayout.BeginFoldoutHeaderGroup(networkFoldState, "Network");
            if(networkFoldState)
            {
                EditorGUI.BeginDisabledGroup(isStarted);
                rpcOscConnection.Draw();
                if (isStarted)
                {
                    EditorGUILayout.TextField("Host", rpcOscConnection.Value.host);
                    EditorGUILayout.IntField("Port", rpcOscConnection.Value.port);
                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}

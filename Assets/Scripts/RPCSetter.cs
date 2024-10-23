using OscJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPC
{
    public abstract class RPCSetter : MonoBehaviour
    {
        [SerializeField] OscConnection oscConnection;

        protected OscServer oscServer;

        protected virtual void OnEnable()
        {
            oscServer = new OscServer(oscConnection.port);
            Debug.Log("Initialize server!");
        }

        protected virtual void OnDisable()
        {
            oscServer?.Dispose();
            oscServer = null;
            Debug.Log("Dispose server!");
        }
    }
}

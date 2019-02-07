using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MyTcpListener))]
public class NetworkEventGrabber : MonoBehaviour {
    private MyTcpListener listener;
    public Action<JSONNode> onMessage;
	// Use this for initialization
	void Start () {
        listener = this.GetComponent<MyTcpListener>();
	}
	
	// Update is called once per frame
	void Update () {
        JSONNode node = listener.getObject();
        if (node != null)
        {
            onMessage(node);
        }
	}
}

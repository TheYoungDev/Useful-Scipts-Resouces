using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NetworkDataSync : NetworkManager
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Detect when a client connects to the Server
    public override void OnClientConnect(NetworkConnection connection)
    {

        //Change the text to show the connection on the client side
       // m_ClientText.text = " " + connection.connectionId + " Connected!";
    }

    //Detect when a client connects to the Server
    public override void OnClientDisconnect(NetworkConnection connection)
    {
        //Change the text to show the connection loss on the client side
        //m_ClientText.text = "Connection" + connection.connectionId + " Lost!";
    }
}

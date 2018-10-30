using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkPlayerManager : NetworkBehaviour{

    

    // Use this for initialization
    void Start()
    {
        //check if local player
        if (!isLocalPlayer)
        {
            return;
        
        }
        CmdSpawnMyPlayer();

    }
    public GameObject NetworkPlayerPrefab;


    // Update is called once per frame
    void Update()
    {

    }
    
    [Command]
    void CmdSpawnMyPlayer()
    {
        GameObject go = Instantiate(NetworkPlayerPrefab);
        go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
}

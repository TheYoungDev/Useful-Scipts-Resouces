using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkPlayerManager : NetworkBehaviour{

    
    //public Transform StartPos;
    // Use this for initialization
    void Start()
    {
        //check if local player
        if (!isLocalPlayer)
        {
            return;
            
            //NetworkServer.Spawn(PlayerPrefab);
        }
        CmdSpawnMyPlayer();


        //Instantiate(PlayerPrefab);
        //Network.Instantiate(PlayerPrefab, StartPos.position, StartPos.rotation,1);
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
        
        //NetworkServer.SpawnWithClientAuthority(treeGo, conn);
        //NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
}

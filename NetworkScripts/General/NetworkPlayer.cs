using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{
    public bool Connected = false;
    public GameObject PlayerCam;
    public GameObject[] DisabledObjects;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (Connected)
        {
            return;
        }
        if (!hasAuthority)
        {
            Debug.LogWarningFormat("Authority: " + hasAuthority);
            gameObject.GetComponent<playerNormalMovement>().enabled = false;
            gameObject.GetComponent<PlayerObjectController>().enabled = false;
            PlayerCam.GetComponent<Camera>().enabled = false;
           // gameObject.GetComponent<NetworkPickupObj>().enabled = false;
            gameObject.layer = 17;
            foreach(GameObject item in DisabledObjects)
            {
                item.gameObject.SetActive(false);
            }
            Connected = true;
            //return;
        }
        if (hasAuthority)
        {
            Debug.LogWarningFormat("Authority: " + hasAuthority);
            gameObject.GetComponent<playerNormalMovement>().enabled = true;
            gameObject.GetComponent<PlayerObjectController>().enabled = true;
            //gameObject.GetComponent<NetworkPickupObj>().enabled = true;
            Connected = true;
            // gameObject.layer = 2;
            //return;
        }
    }
}

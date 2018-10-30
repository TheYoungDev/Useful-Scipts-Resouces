using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObject : NetworkBehaviour
{
    public GameObject PickUp;
    public GameObject PickUpItem;
    public GameObject[] PickUps;
    public Transform playerCam;
    public Camera mainCamera;
    public float throwForce = 100f;

    public bool hasPlayer = false;
    public bool beingCarried = false;
    public bool touched = false;
    public bool isKinematic = false;

    public AudioClip[] soundToPlay;
    private AudioSource audio;
    public int dmg;



    void Update()
    {
        if (PickUp == null)
            SetPickUp();

        if (PickUp != null && hasAuthority)
            {
            if (Input.GetButtonDown("Fire1") && !beingCarried)
            {
                PickUps = GameObject.FindGameObjectsWithTag("PickUp");
                SetPickUp();
            }
            float dist = Vector3.Distance(gameObject.transform.position, PickUp.transform.position);
            if (dist <= 3f && dist >= 0.5f)
            {
                
                hasPlayer = true;

            }
            else
            {
                hasPlayer = false;
            }
            // Pickup item
            if (hasPlayer && Input.GetButtonDown("Fire1") && !beingCarried)//&& !beingCarried
            {
                SetPickUp();
                PickUp.GetComponent<BoxCollider>().size = new Vector3(1.3f, 1.3f, 1.3f);
                CmdPickupItem(gameObject, PickUp);
                beingCarried = true;

            }
            //Check if holding object
            if (beingCarried)
            {
                //check if object hit enviroment
                if (touched)
                {
                    //drop if it touched the wall
                    CmdDropItem(gameObject, PickUp, PickUp.gameObject.transform.position, PickUp.gameObject.transform.rotation);
                    PickUp.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);
                    touched = false;
                }
                //Right Click to throw object
                if (Input.GetButtonUp("Fire2"))
                {
                    CmdThrowItem(gameObject, PickUp, PickUp.gameObject.transform.position, PickUp.gameObject.transform.rotation, gameObject.transform.GetChild(1).GetChild(0).forward * throwForce);
                    beingCarried = false;

                }
                //LeftClick to drop item
                else if (Input.GetButtonUp("Fire1") )
                {
                    CmdDropItem(gameObject, PickUp, PickUp.gameObject.transform.position, PickUp.gameObject.transform.rotation);
                    beingCarried = false;
                    if (!isServer && hasAuthority)

                }

            }
        }
    }
    //set pickup object
    public void SetPickUp()
    {
        //Raycast center of camera
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //check if the object is close enough and is an object that can be pickedup
            if (hit.distance <= 3 && hit.collider.gameObject.CompareTag("PickUp"))
            {
                PickUp = hit.collider.gameObject;
            }
        }
    }


    //Spawn PickupItem
    [Command]
    void CmdSpawnItem()
    {
        //guaranteed to be on the server right now.
        GameObject go = Instantiate(PickUpItem);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        Network.Destroy(PickUp);

    }

    //Tell server item was picked up
    [Command]
    void CmdPickupItem(GameObject _Player, GameObject _PickUp)
    {
        _PickUp.GetComponent<Rigidbody>().isKinematic = true;
        _PickUp.gameObject.transform.position = new Vector3(_Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.x, _Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.y, _Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.z);
        _PickUp.transform.parent = _Player.transform.GetChild(1).GetChild(0);ue;
        _PickUp.layer = 25;

        if (_PickUp.name.Contains("Laser"))
        {
            _PickUp.gameObject.transform.rotation = _Player.transform.rotation;
        }
        RpcPickupItem(_Player, _PickUp);

    }
    //Tell all clients the item was picked up
    [ClientRpc]
    void RpcPickupItem(GameObject _Player, GameObject _PickUp)
    {
        _PickUp.GetComponent<Rigidbody>().isKinematic = true;
        _PickUp.gameObject.transform.position = new Vector3(_Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.x, _Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.y, _Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.z);
        _PickUp.transform.parent = _Player.transform.GetChild(1).GetChild(0);
        _PickUp.layer = 25;

        if (_PickUp.name.Contains("Laser"))
        {
            _PickUp.gameObject.transform.rotation = _Player.transform.rotation;
        }


    }
    //Tell server item was dropped up
    [Command]
    void CmdDropItem(GameObject _Player, GameObject _PickUp, Vector3 _Position, Quaternion _Rotation)
    {
        //sync pos, rot, etc
        _PickUp.GetComponent<Rigidbody>().isKinematic = false;
        _PickUp.transform.parent = null;
        _PickUp.layer = 0;
        _PickUp.gameObject.transform.position = _Position; 
        _PickUp.gameObject.transform.rotation = _Rotation;
        RpcDropItem(_Player, _PickUp, _Position, _Rotation);
        
    }
    //Tell clients item was dropped up
    [ClientRpc]
    void RpcDropItem(GameObject _Player, GameObject _PickUp, Vector3 _Position, Quaternion _Rotation)
    {

        _PickUp.GetComponent<Rigidbody>().isKinematic = false;
        _PickUp.transform.parent = null;
        _PickUp.layer = 0;
        _PickUp.gameObject.transform.position = _Position; 
        _PickUp.gameObject.transform.rotation = _Rotation;

    }
    //Tell server item was thrown
    [Command]
    void CmdThrowItem(GameObject _Player, GameObject _PickUp, Vector3 _Position, Quaternion _Rotation, Vector3 _Force)
    {

        _PickUp.GetComponent<Rigidbody>().isKinematic = false;
        _PickUp.transform.parent = null;

        _PickUp.layer = 0;

        _PickUp.gameObject.transform.position = new Vector3(_Position.x, _Position.y, _Position.z);
        _PickUp.gameObject.transform.rotation = _Rotation;

        _PickUp.GetComponent<Rigidbody>().AddForce(_Force);
        RpcThrowItem(_Player, _PickUp, _Position, _Rotation, _Force);


    }
    //Tell clients item was thrown
    [ClientRpc]
    void RpcThrowItem(GameObject _Player, GameObject _PickUp, Vector3 _Position, Quaternion _Rotation, Vector3 _Force)
    {
        if (isServer)
            return;
        _PickUp.GetComponent<Rigidbody>().isKinematic = false;
        _PickUp.transform.parent = null;
        _PickUp.layer = 0;
        _PickUp.gameObject.transform.position = new Vector3(_Position.x, _Position.y, _Position.z);
        _PickUp.gameObject.transform.rotation = _Rotation;
        _PickUp.GetComponent<Rigidbody>().AddForce(_Force);

    }
}
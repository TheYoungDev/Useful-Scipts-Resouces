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

    // Use this for initialization
    void Start()
    {
        AddPickup();
    }

    // Update is called once per frame
    void Update()
    {
        if (PickUp == null)
            //if (PickUp == null && Input.GetButtonDown("Fire1") && hasAuthority)
            //AddPickup();
            SetPickUp();

        if (PickUp != null && hasAuthority)
            {
            if (Input.GetButtonDown("Fire1") && !beingCarried)//&& !beingCarried
            {
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
               /// if(!isClient)
                //RpcPickupItem(gameObject, PickUp); //
                beingCarried = true;
                if (!isServer && hasAuthority)
                {/*
                    //PickUp.GetComponent<NetworkTransform>().enabled = false;
                    PickUp.GetComponent<Rigidbody>().isKinematic = true;
                    PickUp.gameObject.transform.position = new Vector3(gameObject.transform.GetChild(1).GetChild(0).gameObject.transform.position.x, gameObject.transform.GetChild(1).GetChild(0).gameObject.transform.position.y, gameObject.transform.GetChild(1).GetChild(0).gameObject.transform.position.z);
                    PickUp.transform.parent = gameObject.transform.GetChild(1).GetChild(0);
                    //PickUp.gameObject.GetComponent<Collider>().isTrigger = true;
                    PickUp.layer = 25;
                    //PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);

                    // PickUp.gameObject.GetComponent<Collider>().isTrigger = true;
                    //PickUp.GetComponent<NetworkTransform>().enabled = false;
                    /*
                    PickUp.GetComponent<Rigidbody>().isKinematic = true;
                    PickUp.transform.parent = gameObject.transform.GetChild(1);
                    PickUp.gameObject.GetComponent<Collider>().isTrigger = true;
                    PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
                    beingCarried = true;*/
                }
            }
            if (beingCarried)
            {
                if (touched)
                {
                    CmdDropItem(gameObject, PickUp, PickUp.gameObject.transform.position, PickUp.gameObject.transform.rotation);
                    PickUp.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);
                    touched = false;
                }
                if (Input.GetButtonUp("Fire2"))
                {
                   
                    CmdThrowItem(gameObject, PickUp, PickUp.gameObject.transform.position, PickUp.gameObject.transform.rotation, gameObject.transform.GetChild(1).GetChild(0).forward * throwForce);
                    
                    //if (!isClient)
                    //    RpcThrowItem(gameObject, PickUp);
                    beingCarried = false;
                    if (!isServer && hasAuthority)
                    {
                        /*
                        PickUp.GetComponent<Rigidbody>().isKinematic = false;

                        PickUp.transform.parent = null;
                        //PickUp.gameObject.GetComponent<Collider>().isTrigger = false;
                        PickUp.layer = 25;
                        // _PickUp.layer = 2; 
                        // _PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
                        PickUp.gameObject.transform.position = new Vector3(PickUp.gameObject.transform.position.x, PickUp.gameObject.transform.position.y, PickUp.gameObject.transform.position.z);
                        PickUp.GetComponent<Rigidbody>().AddForce(gameObject.transform.GetChild(1).GetChild(0).forward * throwForce);
                        //PickUp.GetComponent<NetworkTransform>().enabled = true;
                        //PickUp.gameObject.GetComponent<Collider>().isTrigger = false;
                        //PickUp.GetComponent<NetworkTransform>().enabled = true;
                        /*
                        PickUp.GetComponent<Rigidbody>().isKinematic = true;
                        PickUp.transform.parent = gameObject.transform.GetChild(1);
                        PickUp.gameObject.GetComponent<Collider>().isTrigger = true;
                        PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
                        beingCarried = true;*/
                    }

                }
                else if (Input.GetButtonUp("Fire1") )
                {
                    //(PickUp.gameObject.transform.position.x, PickUp.gameObject.transform.position.y, PickUp.gameObject.transform.position.z);
                    CmdDropItem(gameObject, PickUp, PickUp.gameObject.transform.position, PickUp.gameObject.transform.rotation);
                   // if (!isClient)
                      //  RpcDropItem(gameObject, PickUp);
                    beingCarried = false;
                    if (!isServer && hasAuthority)
                    {/*
                        //PickUp.GetComponent<NetworkTransform>().enabled = true;
                        PickUp.GetComponent<Rigidbody>().isKinematic = false;
                        PickUp.transform.parent = null;
                        // PickUp.gameObject.GetComponent<Collider>().isTrigger = false;
                        PickUp.layer = 25;
                        PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
                        PickUp.gameObject.transform.position = new Vector3(PickUp.gameObject.transform.position.x, PickUp.gameObject.transform.position.y, PickUp.gameObject.transform.position.z);
                        //PickUp.GetComponent<NetworkTransform>().enabled = true;
                        //PickUp.gameObject.GetComponent<Collider>().isTrigger = false;
                        //PickUp.GetComponent<NetworkTransform>().enabled = true;
                        /*
                        PickUp.GetComponent<Rigidbody>().isKinematic = true;
                        PickUp.transform.parent = gameObject.transform.GetChild(1);
                        PickUp.gameObject.GetComponent<Collider>().isTrigger = true;
                        PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
                        beingCarried = true;*/
                    }
                }
                else
                {
                    //AddPickup();
                }

            }
        }
    }
    public void SetPickUp()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= 3 && hit.collider.gameObject.CompareTag("PickUp"))
            {
                PickUp = hit.collider.gameObject;
            }
        }
    }
    public void AddPickup()
    {
        Vector3 _position = transform.position;
        float _distance = Mathf.Infinity;

        foreach (GameObject _pickup in PickUps)
        {
            Vector3 diff = _pickup.transform.position - _position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < _distance)
            {
                PickUp = _pickup;
                _distance = curDistance;
            }

        }
        //playerCam = gameObject.transform.GetChild(1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PickUps = GameObject.FindGameObjectsWithTag("PickUp");
            //if(!beingCarried)
               // AddPickup();
        }
        if (beingCarried && (other.gameObject.CompareTag("Wall") || other.gameObject.tag.Contains("Door")))
        {
          //  print(other.gameObject.tag);
          //  touched = true;

        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            //AddPickup();
        }
    }


    [Command]
    void CmdSpawnItem()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(PickUpItem);

        //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        Network.Destroy(PickUp);

    }

    [Command]
    void CmdPickupItem(GameObject _Player, GameObject _PickUp)
    {
        //disable pickup item
        //enable vissual for other players to see
        //enable pickup item and broadcast location

        _PickUp.GetComponent<Rigidbody>().isKinematic = true;
        //_PickUp.GetComponent<NetworkTransform>().enabled = false;
        _PickUp.gameObject.transform.position = new Vector3(_Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.x, _Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.y, _Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.z);
        _PickUp.transform.parent = _Player.transform.GetChild(1).GetChild(0);
        //_PickUp.gameObject.GetComponent<Collider>().isTrigger = true;
        _PickUp.layer = 25;
        //_PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
        //PickUp.gameObject.transform.position = new Vector3(PickUp.gameObject.transform.position.x, PickUp.gameObject.transform.position.y, PickUp.gameObject.transform.position.z);
        //beingCarried = true;
        //touched = false;
        if (_PickUp.name.Contains("Laser"))
        {
            _PickUp.gameObject.transform.rotation = _Player.transform.rotation;
        }


        RpcPickupItem(_Player, _PickUp);

    }
    [ClientRpc]
    void RpcPickupItem(GameObject _Player, GameObject _PickUp)
    {
       // if (hasAuthority)
       //     return;
            //disable pickup item
            //enable vissual for other players to see
            //enable pickup item and broadcast location

        _PickUp.GetComponent<Rigidbody>().isKinematic = true;
        //_PickUp.GetComponent<NetworkTransform>().enabled = false;
        _PickUp.gameObject.transform.position = new Vector3(_Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.x, _Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.y, _Player.transform.GetChild(1).GetChild(0).gameObject.transform.position.z);
        _PickUp.transform.parent = _Player.transform.GetChild(1).GetChild(0);
        //_PickUp.gameObject.GetComponent<Collider>().isTrigger = true;
        _PickUp.layer = 25;
        // _PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
        if (_PickUp.name.Contains("Laser"))
        {
            _PickUp.gameObject.transform.rotation = _Player.transform.rotation;
        }
        //PickUp.gameObject.transform.position = new Vector3(PickUp.gameObject.transform.position.x, PickUp.gameObject.transform.position.y, PickUp.gameObject.transform.position.z);
        // beingCarried = true;
        //touched = false;

    }

    [Command]
    void CmdDropItem(GameObject _Player, GameObject _PickUp, Vector3 _Position, Quaternion _Rotation)
    {

        _PickUp.GetComponent<Rigidbody>().isKinematic = false;
        _PickUp.transform.parent = null;
       // _PickUp.gameObject.GetComponent<Collider>().isTrigger = false;
        // _PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
        _PickUp.layer = 0;
        _PickUp.gameObject.transform.position = _Position; // new Vector3(PickUp.gameObject.transform.position.x, PickUp.gameObject.transform.position.y, PickUp.gameObject.transform.position.z);
        _PickUp.gameObject.transform.rotation = _Rotation;
        //_PickUp.GetComponent<NetworkTransform>().enabled = true;
        // beingCarried = false;
        // touched = false;
        // _Player.transform.GetChild(1);
        
        RpcDropItem(_Player, _PickUp, _Position, _Rotation);
        
    }
    [ClientRpc]
    void RpcDropItem(GameObject _Player, GameObject _PickUp, Vector3 _Position, Quaternion _Rotation)
    {
        // if (hasAuthority)
        //     return;
        _PickUp.GetComponent<Rigidbody>().isKinematic = false;
        _PickUp.transform.parent = null;
        //_PickUp.gameObject.GetComponent<Collider>().isTrigger = false;
        _PickUp.layer = 0;
        // _PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
        //_PickUp.gameObject.transform.position = new Vector3(_PickUp.gameObject.transform.position.x, _PickUp.gameObject.transform.position.y, _PickUp.gameObject.transform.position.z);
        _PickUp.gameObject.transform.position = _Position; // new Vector3(PickUp.gameObject.transform.position.x, PickUp.gameObject.transform.position.y, PickUp.gameObject.transform.position.z);
        _PickUp.gameObject.transform.rotation = _Rotation;
        // _PickUp.GetComponent<NetworkTransform>().enabled = true;
        // beingCarried = false;
        // touched = false;
        // _Player.transform.GetChild(1);

    }

    [Command]
    void CmdThrowItem(GameObject _Player, GameObject _PickUp, Vector3 _Position, Quaternion _Rotation, Vector3 _Force)
    {
        //pass direction and force
        //rotation

        _PickUp.GetComponent<Rigidbody>().isKinematic = false;
        _PickUp.transform.parent = null;
        //_PickUp.gameObject.GetComponent<Collider>().isTrigger = false;
        _PickUp.layer = 0;
        // _PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
        _PickUp.gameObject.transform.position = new Vector3(_Position.x, _Position.y, _Position.z);
        _PickUp.gameObject.transform.rotation = _Rotation;
        //beingCarried = false;
        //touched = false;
        _PickUp.GetComponent<Rigidbody>().AddForce(_Force);
        RpcThrowItem(_Player, _PickUp, _Position, _Rotation, _Force);
        // _PickUp.GetComponent<NetworkTransform>().enabled = true;

    }
    [ClientRpc]
    void RpcThrowItem(GameObject _Player, GameObject _PickUp, Vector3 _Position, Quaternion _Rotation, Vector3 _Force)
    {
        if (isServer)
            return;
        _PickUp.GetComponent<Rigidbody>().isKinematic = false;
        _PickUp.transform.parent = null;
        //_PickUp.gameObject.GetComponent<Collider>().isTrigger = false;
        _PickUp.layer = 0;
        // _PickUp.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
        _PickUp.gameObject.transform.position = new Vector3(_Position.x, _Position.y, _Position.z);
        _PickUp.gameObject.transform.rotation = _Rotation;
        _PickUp.GetComponent<Rigidbody>().AddForce(_Force);
        //_PickUp.GetComponent<NetworkTransform>().enabled = true;
    }
}
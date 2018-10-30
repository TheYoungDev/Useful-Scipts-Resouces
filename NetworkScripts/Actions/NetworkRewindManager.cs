using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;


public class NetworkRewindManager : NetworkBehaviour
{

    public bool TimeReverse = false;
    public bool RewindTime = false;
    public bool Pause = false;
    public bool canPause = false;
    public bool PlatForm = false;
    public bool CoolDownOver = true;

    public GameObject[] RewindObjects;

    public List<PositionRotation> PositionRotations;
    public List<List<PositionRotation>> PositionRotationsList;
    Vector3 ZAxis;
    Rigidbody rb;
    public int RewindSecs = 60;
    public int PauseLimit = 20;
    public int CoolDownTime = 10;
    public PositionRotation PositionRotation;

    // Use this for initialization
    void Start()
    {
        GameObject[] Temp1 = GameObject.FindGameObjectsWithTag("PickUp");
        GameObject[] Temp2 = GameObject.FindGameObjectsWithTag("PlatForm");                                                        
        RewindObjects = Temp1.Concat(Temp2).ToArray();

        if (RewindObjects.Length == 0)
            return;
        int i = 0;
        PositionRotationsList = new List<List<PositionRotation>>();
        foreach (GameObject item in RewindObjects) { 
            PositionRotations = new List<PositionRotation>();
            if (GetComponent<Rigidbody>())
            {
                rb = GetComponent<Rigidbody>();
            }
            PositionRotation = new PositionRotation(item.transform.position, item.transform.rotation); //add? to list1
            PositionRotations.Add(PositionRotation);
            PositionRotationsList.Add(PositionRotations);
       }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPause && Pause)
        {
           
        }
        if (Input.GetButtonDown("Fire2") && TimeReverse)
        {
            
            foreach (GameObject Item in RewindObjects)
            {
                if (isLocalPlayer)
                    CmdSetAuthority(Item.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
                
                Item.GetComponent<NetworkRewindObject>().StartRewind();
            }

        }
        if (Input.GetButtonUp("Fire2") && TimeReverse)
        {

            foreach (GameObject Item in RewindObjects)
            {

                Item.GetComponent<NetworkRewindObject>().StopRewind();
                if (isLocalPlayer)
                    CmdRemoveAuthority(Item.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
               
            }

        }
        if (Input.GetButtonDown("Fire2") && canPause)
        {
            foreach (GameObject Item in RewindObjects)
            {
                bool TempBool = false;
                if (isLocalPlayer)
                    CmdSetAuthority(Item.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
                Debug.Log("Test1");
            if (Item.CompareTag("PickUp")) {
                    if (Item.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.None)
                        TempBool = true;
                }
            if (Item.CompareTag("PlatForm")){
                if(Item.GetComponent<MoveingPlatform>().isMoving == true)
                        TempBool = true;
                }
                CmdPauseTime(TempBool, Item);

            }

        }
        if (Input.GetButtonUp("Fire2") && canPause)
        {
            foreach (GameObject Item in RewindObjects)
            {
                if (isLocalPlayer)
                    CmdRemoveAuthority(Item.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());

            
            }

        }


    }
    void FixedUpdate()
    {
        if (RewindTime)
        {
            CmdRewind();
        }
        if (!RewindTime )
        {
            CmdRecord();
        }
    }

    [Command]
    void CmdSetAuthority(NetworkIdentity grabID, NetworkIdentity playerID)
    {
        grabID.AssignClientAuthority(connectionToClient);
    }
    [Command]
    void CmdRemoveAuthority(NetworkIdentity grabID, NetworkIdentity playerID)
    {
        grabID.RemoveClientAuthority(connectionToClient);
    }

    [Command]
    void CmdRecord()
    {
        int i = 0;
        foreach(GameObject item in RewindObjects) {
        if (PositionRotationsList[i].Count > Mathf.Round(RewindSecs * 1f / Time.fixedDeltaTime))// 2000
        {
                PositionRotationsList[i].RemoveAt(PositionRotations.Count - 1);
        }
        if (!Pause && PositionRotation.position != item.transform.position)
        { 

                PositionRotationsList[i].Insert(0, new PositionRotation(item.transform.position, item.transform.rotation));
        }
        PositionRotation = new PositionRotation(item.transform.position, item.transform.rotation);
        i++;
        }
    }

    [ClientRpc]
    public void RpcRewind(GameObject _item, Vector3 _position,Quaternion _rotation)
    {
        _item.transform.position = _position;
        _item.transform.rotation = _rotation;
    }

    [Command]
    public void CmdRewind()
    {
        int i = 0;
        foreach (GameObject item in RewindObjects)
        {
            if (PositionRotationsList[i].Count > 0)
            {
                PositionRotation pointInTime = PositionRotationsList[i][0];
                item.transform.position = pointInTime.position;
                item.transform.rotation = pointInTime.rotation;
                RpcRewind(item, item.transform.position, item.transform.rotation);
                PositionRotationsList[i].RemoveAt(0);
            }
            else
            {
                CmdStopRewind();

            }
            i++;
        }
    }

    [Command]
    public void CmdStartRewind()
    {
      
        foreach (GameObject item in RewindObjects)
        {
            if (item.CompareTag("PlatForm"))
            {

                item.GetComponent<MoveingPlatform>().isMoving = false;
            }

            if (item.CompareTag("PickUp"))
            {
                item.GetComponent<Rigidbody>().isKinematic = true;
            }
            
        }
        RewindTime = true;
        RpcStartRewind();
    }
    [ClientRpc]
    public void RpcStartRewind()
    {

        foreach (GameObject item in RewindObjects)
        {

            if (item.CompareTag("PlatForm"))
            {

                item.GetComponent<MoveingPlatform>().isMoving = false;
            }
            if (item.CompareTag("PickUp"))
            {
                item.GetComponent<Rigidbody>().isKinematic = true;
            }
            
        }
        RewindTime = true;
    }

    public void StartRewind()
    {
        int i = 0;
        foreach (GameObject item in RewindObjects)
        {

            if (item.CompareTag("PlatForm"))
            {
                item.GetComponent<MoveingPlatform>().isMoving = false;
            }



            if (item.CompareTag("PickUp"))
            {
                item.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        RewindTime = true;
    }


    [Command]
    public void CmdStopRewind()
    {
        RewindTime = false;
        foreach (GameObject item in RewindObjects)
        {

            if (item.CompareTag("PlatForm"))
            {
                gameObject.GetComponent<MoveingPlatform>().StartMoving();

            }

            if (item.CompareTag("PickUp"))
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        RpcStopRewind();

    }
    [ClientRpc]
    public void RpcStopRewind()
    {
        RewindTime = false;
        foreach (GameObject item in RewindObjects)
        {

            if (item.CompareTag("PlatForm"))
            {
                gameObject.GetComponent<MoveingPlatform>().StartMoving();

            }

            if (item.CompareTag("PickUp"))
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }
    public void StopRewind()
    {
        RewindTime = false;
        foreach (GameObject item in RewindObjects)
        {

            if (item.CompareTag("PlatForm"))
            {
                gameObject.GetComponent<MoveingPlatform>().StartMoving();

            }

            if (item.CompareTag("PickUp"))
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }


    [Command]
    public void CmdPauseTime(bool _Pause, GameObject Item)
    {

        if (_Pause)
        {
            if (Item.CompareTag("PickUp"))
            {
            
                Item.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            if (Item.CompareTag("PlatForm"))
            {
                Item.GetComponent<MoveingPlatform>().isMoving = false;
            }

        }
        if (!_Pause)
        {

            if (Item.CompareTag("PickUp"))
            {
                Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            if (Item.CompareTag("PlatForm"))
            {
                Item.GetComponent<MoveingPlatform>().isMoving = true;
            }
         
        }
        RpcPauseTime(_Pause, Item);

    }
    [ClientRpc]
    public void RpcPauseTime(bool _Pause, GameObject Item)
    {

        if (_Pause)
        {
            if (Item.CompareTag("PickUp"))
            {
               
                Item.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            if (Item.CompareTag("PlatForm"))
            {
                Item.GetComponent<MoveingPlatform>().isMoving = false;
            }

        }
        if (!_Pause)
        {

            if (Item.CompareTag("PickUp"))
            { 
                Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            if (Item.CompareTag("PlatForm"))
            {
                Item.GetComponent<MoveingPlatform>().isMoving = true;
            }

        }


    }
    public void PauseTime(bool _Pause)
    {
        if (_Pause)
        {
            int i = 0;
            foreach (GameObject item in RewindObjects)
            {
                if (item.CompareTag("PickUp"))
                {
                   
                    item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                if (item.CompareTag("PlatForm"))
                {
                    
                }
            }

        }
        if (!_Pause)
        {
            foreach (GameObject item in RewindObjects)
            {
                if (item.CompareTag("PickUp"))
                {
                    item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
                if (item.CompareTag("PlatForm"))
                {
                    
                }
            }
        }
    }

    IEnumerator PauseTimeout()
    {
        print(Time.time);
        yield return new WaitForSeconds(PauseLimit);
        if (Pause)
        {
            //CmdPauseTime(Pause);
        }


        print(Time.time);
    }


}


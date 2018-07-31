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
   // public 
    //public GameObject[] tempArray2;

    public GameObject[] RewindObjects;

    public List<PositionRotation> PositionRotations;
    public List<List<PositionRotation>> PositionRotationsList;
    Vector3 ZAxis;
    Rigidbody rb;
    public int RewindSecs = 60;
    public int PauseLimit = 20;
    public int CoolDownTime = 10;
    public bool MouseHit = false; //can be removed
    public PositionRotation PositionRotation;

    // Use this for initialization
    void Start()
    {
        GameObject[] Temp1 = GameObject.FindGameObjectsWithTag("PickUp");
        GameObject[] Temp2 = GameObject.FindGameObjectsWithTag("PlatForm");
        //RewindObjects = GameObject.FindGameObjectsWithTag("PickUp");//.Concat(tempArray2).ToArray();                                                          // }
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
            //CmdPauseTime();
        }
        if (Input.GetButtonDown("Fire2") && TimeReverse)
        {
            //Debug.Log("rewind1");
            foreach (GameObject Item in RewindObjects)
            {
                if (isLocalPlayer)
                    CmdSetAuthority(Item.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
                //Item.GetComponent<NetworkRewindObject>().CmdTest();
                Item.GetComponent<NetworkRewindObject>().StartRewind();
            }
            //Debug.Log("test1");
            //CmdStartRewind();
            /*RpcStartRewind();
            if (!isServer && hasAuthority)
                StartRewind();*/
        }
        if (Input.GetButtonUp("Fire2") && TimeReverse)
        {
            // Debug.LogErrorFormat("test2" );
            foreach (GameObject Item in RewindObjects)
            {
                //if(!isServer)
                    //CmdRemoveAuthority(Item.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
                Item.GetComponent<NetworkRewindObject>().StopRewind();
                if (isLocalPlayer)
                    CmdRemoveAuthority(Item.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
                //Item.GetComponent<NetworkRewindObject>().CmdTest();
            }
            //CmdStopRewind();
            /*RpcStopRewind();
            if (!isServer && hasAuthority)
                StopRewind();*/
        }
        if (Input.GetButtonDown("Fire2") && canPause)
        {

            //Pause = !Pause;
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
               // Item.GetComponent<NetworkRewindObject>().PauseTime();
            }
            //Debug.Log("test3"+ Pause);

           // PauseTime();
            /*RpcPauseTime(Pause);
            if (!isServer && hasAuthority)
                PauseTime(Pause);*/
        }
        if (Input.GetButtonUp("Fire2") && canPause)
        {
            //Pause = !Pause;
            foreach (GameObject Item in RewindObjects)
            {
                if (isLocalPlayer)
                    CmdRemoveAuthority(Item.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());

               // Item.GetComponent<NetworkRewindObject>().PauseTime();
            }
            //Debug.Log("test3"+ Pause);

            // PauseTime();
            /*RpcPauseTime(Pause);
            if (!isServer && hasAuthority)
                PauseTime(Pause);*/
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
        { //&& temp.position != transform.position

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
    /*[Command]
    public void CmdServerRewind(GameObject _item, Vector3 _position, Quaternion _rotation)
    {
        _item.transform.position = _position;
        _item.transform.rotation = _rotation;
    }*/
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
                /*RpcStopRewind();
                if (!isServer && hasAuthority)
                    StopRewind();*/
            }
            i++;
        }
    }

    [Command]
    public void CmdStartRewind()
    {
        //Debug.Log("test2");
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
        //Debug.Log("test3");
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
    public void StartRewindObj(GameObject HitObject)
    {/*
        int i = 0;
        foreach (GameObject item in RewindObjects)
        {

            if (item.CompareTag("PlatForm"))
            {

                // item.GetComponent<MoveingPlatform>().isMoving = false;
            }
            RewindTime = true;


            if (item.CompareTag("PickUp"))
            {
                item.GetComponent<Rigidbody>().isKinematic = true;
            }
        }*/
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
        // CoolDownOver = false;
        // StartCoroutine(CoolDown());
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
       

        // CoolDownOver = false;
        // StartCoroutine(CoolDown());
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
        

        // CoolDownOver = false;
        // StartCoroutine(CoolDown());
    }
    public void StopRewindObj(GameObject HitObject)
    {
        /*
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
        */

        // CoolDownOver = false;
        // StartCoroutine(CoolDown());
    }

    [Command]
    public void CmdPauseTime(bool _Pause, GameObject Item)
    {
        //Debug.Log("freeze0"+ Pause);
        if (_Pause)
        {
            if (Item.CompareTag("PickUp"))
            {
                //Debug.Log("freeze1");
                Item.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            if (Item.CompareTag("PlatForm"))
            {
                Item.GetComponent<MoveingPlatform>().isMoving = false;
            }
            //int i = 0;
            /* foreach (GameObject item in RewindObjects)
             {
                 if (item.CompareTag("PickUp"))
                 {
                     //Debug.Log("freeze1");
                     item.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                 }
                 if (item.CompareTag("PlatForm"))
                 {
                     item.GetComponent<MoveingPlatform>().isMoving = false;
                 }
             }*/
        }
        if (!_Pause)
        {
           // foreach (GameObject item in RewindObjects)
           // {
                if (Item.CompareTag("PickUp"))
                {
                // Debug.LogErrorFormat("unfreeze0");
                    Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
                if (Item.CompareTag("PlatForm"))
                {
                    Item.GetComponent<MoveingPlatform>().isMoving = true;
                }
            //}
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
                //Debug.Log("freeze1");
                Item.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            if (Item.CompareTag("PlatForm"))
            {
                Item.GetComponent<MoveingPlatform>().isMoving = false;
            }
            //int i = 0;
            /* foreach (GameObject item in RewindObjects)
             {
                 if (item.CompareTag("PickUp"))
                 {
                     //Debug.Log("freeze1");
                     item.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                 }
                 if (item.CompareTag("PlatForm"))
                 {
                     item.GetComponent<MoveingPlatform>().isMoving = false;
                 }
             }*/
        }
        if (!_Pause)
        {
            // foreach (GameObject item in RewindObjects)
            // {
            if (Item.CompareTag("PickUp"))
            {
                // Debug.LogErrorFormat("unfreeze0");
                Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            if (Item.CompareTag("PlatForm"))
            {
                Item.GetComponent<MoveingPlatform>().isMoving = true;
            }
            //}
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
                    //Debug.LogErrorFormat("freeze");
                    item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                if (item.CompareTag("PlatForm"))
                {
                    // item.GetComponent<MoveingPlatform>().isMoving = false;
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
                    //item.GetComponent<MoveingPlatform>().isMoving = true;
                }
            }
        }
    }
    public void PauseTimeObj(GameObject HitObject)
    {/*
        Debug.LogErrorFormat("freeze0");
        Pause = !Pause;
        if (Pause)
        {
            int i = 0;
            foreach (GameObject item in RewindObjects)
            {
                if (item.CompareTag("PickUp"))
                {
                    Debug.LogErrorFormat("freeze");
                    item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                if (item.CompareTag("PlatForm"))
                {
                    // item.GetComponent<MoveingPlatform>().isMoving = false;
                }
            }

        }
        if (!Pause)
        {

            foreach (GameObject item in RewindObjects)
            {
                if (item.CompareTag("PickUp"))
                {
                    item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
                if (item.CompareTag("PlatForm"))
                {
                    //item.GetComponent<MoveingPlatform>().isMoving = true;
                }
            }


        }*/


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

    /*IEnumerator CoolDown()
    {
        print(Time.time);
        yield return new WaitForSeconds(CoolDownTime);
        CoolDownOver = true;

        print(Time.time);
    }*/
}


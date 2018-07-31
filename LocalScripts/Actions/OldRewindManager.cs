using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldRewindManager : MonoBehaviour
{
    public bool TimeReverse = false;
    public bool RewindTime = false;
    public bool Pause = false;
    public bool canPause = false;
    public bool PlatForm = false;
    public bool CoolDownOver = true;

    List<PositionRotation> PositionRotation;
    Vector3 ZAxis;
    Rigidbody rb;
    public int RewindSecs = 60;
    public int PauseLimit = 20;
    public int CoolDownTime = 10;
    public bool SinglePlayer = true; //can be removed
    public bool MouseHit = false;
    public GameObject PlayerObject;
    PositionRotation temp;
    // public bool isFrozen = false;

    // Use this for initialization
    void Start()
    {
        PositionRotation = new List<PositionRotation>();
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
        temp = new PositionRotation(transform.position, transform.rotation);
        if (SinglePlayer)
        {
            PlayerObject = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
        if ( PlayerObject.GetComponent<LocalPlayerMovement>().TimeObjectHit ==false) //Input.GetButton("SecondaryFire")
        {
            TimeReverse =PlayerObject.GetComponent<LocalPlayerMovement>().CanReverse;
            canPause = PlayerObject.GetComponent<LocalPlayerMovement>().CanPauseTime;
        
            if (Input.GetButtonDown("Fire2") && TimeReverse)
            {
                Debug.Log("StartRewind All");
                StartRewind();
            }

            if (Input.GetButtonDown("Fire1") && canPause)
            {
                //MouseHit = false;
                //reset timer?
                Debug.Log("Pause All");
                PauseTime();
            }
        }
        if (Input.GetButtonUp("Fire2") && TimeReverse)
        {
            StopRewind();
        }
       /* if (Input.GetButtonUp("Fire1") && canPause)
        {
            PauseTime();
        }*/
        //if (Input.GetButtonUp("Fire2") && canPause)
        //{
        //MouseHit = false;
        //reset timer?
        //   PauseTime();
        //}
    }
    private void FixedUpdate()
    {
        if (RewindTime)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }
    void Record()
    {
        /*if(temp.position != transform.position)
        {
            return;
        }*/
        if (PositionRotation.Count > Mathf.Round(RewindSecs * 1f / Time.fixedDeltaTime))
        {
            PositionRotation.RemoveAt(PositionRotation.Count - 1);
        }
        if (!Pause)
        {
            PositionRotation.Insert(0, new PositionRotation(transform.position, transform.rotation));
        }
        temp = new PositionRotation(transform.position, transform.rotation);
    }
    public void Rewind()
    {
        if (PositionRotation.Count > 0)
        {
            PositionRotation pointInTime = PositionRotation[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            PositionRotation.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void PauseTime()
    {
        if (!CoolDownOver)
        {
            return;
        }
        if (!canPause)
        {
            return;
        }

        print(Pause);
        Pause = !Pause;
        if (Pause)
        {
            if (MouseHit)
            {
                print("mouse hit");
            }
            else
            {
                if (!PlatForm)
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
                if (PlatForm)
                {
                    //print("hi2");
                    gameObject.GetComponent<MoveingPlatform>().isMoving = false;
                }
                // StartCoroutine(PauseTimeout());
                //rb.constraints = RigidbodyConstraints.None;
            }
        }
        if (!Pause)
        {
            if (MouseHit)
            {
                print("mouse hit");

            }
            else
            {
                if (!PlatForm)
                {
                    rb.constraints = RigidbodyConstraints.None;
                }
                if (PlatForm)
                {
                    //print("hi");
                    gameObject.GetComponent<MoveingPlatform>().isMoving = true;
                }
            }
            // CoolDownOver = false;
            //StartCoroutine(CoolDown());

        }


    }
    IEnumerator PauseTimeout()
    {
        //print(Time.time);
        yield return new WaitForSeconds(PauseLimit);
        if (Pause)
        {
            PauseTime();
        }


        //print(Time.time);
    }

    public void StartRewind()
    {
        /* if (!CoolDownOver)
         {
             return;
         }*/
        RewindTime = true;

        if (PlatForm)
        {
            gameObject.GetComponent<MoveingPlatform>().isMoving = false;
        }
        if (!PlatForm)
        {
            rb.isKinematic = true;
        }
    }
    public void StopRewind()
    {
        RewindTime = false;
        if (PlatForm)
            gameObject.GetComponent<MoveingPlatform>().isMoving = true;
        if (!PlatForm)
        {
            rb.isKinematic = false;
        }
        // CoolDownOver = false;
        //StartCoroutine(CoolDown());
    }
    IEnumerator CoolDown()
    {
        //print(Time.time);
        yield return new WaitForSeconds(CoolDownTime);
        CoolDownOver = true;

       // print(Time.time);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    /*public bool isSwitch = false;
    public bool isRespawner = false;
    public bool isEventTrigger = false;
    public GameObject[] LookupObjects;
    public Vector3[] ObjectPos;
    // public GameObject MsgObject;
    public string Animation1 = "PressurePlateDown";
    public string Animation2 = "PressurePlateDown 0";
    public bool isTriggered = false;
    public bool StopAnim = false;
    public AudioClip Sound1;
    public AudioClip Sound2;
    public GameObject[] Doors;
    public int Interactions = 0;
    public string colour = "Red";
    private Animator anim;*/
    public bool isSwitch = true;
    public bool isRespawner = false;
    public bool isEventTrigger = false;
    public bool isLaserRecieverFlag = false;
    public bool isLaserReciever = false;
    public GameObject[] LookupObjects;
    public Vector3[] ObjectPos;
    public GameObject Shield;
    // public GameObject MsgObject;
    public string Animation1 = "PressurePlateDown";
    public string Animation2 = "PressurePlateDown 0";
    public bool isTriggered = false;
    public bool StopAnim = false;
    public bool Flag = false;
    public AudioClip Sound1;
    public AudioClip Sound2;
    public GameObject[] Doors;
    public int Interactions = 0;
    public string colour = "Red";
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        if (colour != "None")
        {
            FindDoors();
        }

        if (isRespawner)
        {
            int i = 0;
            foreach (GameObject LookupItem in LookupObjects)
            {
                ObjectPos[i] = LookupItem.transform.position;
                i++;
            }
        }

    }
    public void LaserSwitchOn()
    {
        if (isLaserReciever && isLaserRecieverFlag)
        {
            FindDoors();
            isTriggered = true;
            foreach (GameObject door in Doors)
            {
                door.SendMessage("UnlockDoor");
            }
            isLaserRecieverFlag = false;
            anim.SetBool("Trigger", true);
            if (Shield != null && Shield.active == false)
                Shield.SetActive(true);
        }
    }
    public void LaserSwitchOff()
    {
        if (isLaserReciever && !isLaserRecieverFlag)
        {
            FindDoors();
            isTriggered = false;
            foreach (GameObject door in Doors)
            {
                door.SendMessage("LockDoor");
            }
            isLaserRecieverFlag = true;
            anim.SetBool("Trigger", false);
            if (Shield != null && Shield.active == true)
                Shield.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FindDoors(){
        if(colour != "None")
        Doors = GameObject.FindGameObjectsWithTag(colour + "Door");
    }
    void OnTriggerEnter(Collider other)
    {
        
        Interactions++;
        if (Interactions >= 1 && isSwitch == false)
        {
            FindDoors();
            isTriggered = true;
            foreach (GameObject door in Doors)
            {
                door.SendMessage("UnlockDoor");
            }


            //anim.Play(Animation1, -1, 0f);
            anim.SetBool("Triggered", true);
            AudioSource.PlayClipAtPoint(Sound1, transform.position);
        }
        if (isRespawner && other.gameObject.CompareTag("Player"))
        {
            anim.Play(Animation1, -1, 0f);
            AudioSource.PlayClipAtPoint(Sound1, transform.position);
            int i = 0;
            foreach (GameObject LookupItem in LookupObjects)
            {
                LookupItem.transform.position = ObjectPos[i];
                if (LookupItem.CompareTag("PlatForm")){
                    LookupItem.GetComponent<MoveingPlatform>().isMoving = false;
                }
                i++;
            }
           // Object.transform.position = ObjectPos;

        }
        if (isEventTrigger)
        {
            anim.Play(Animation1, -1, 0f);
            AudioSource.PlayClipAtPoint(Sound1, transform.position);
            foreach (GameObject LookupItem in LookupObjects)
            {
                LookupItem.GetComponent<MoveingPlatform>().isMoving = false;
            }
                

        }



    }
    public void ActivateSwitch()
    {
        anim.SetBool("Trigger", true);
        anim.SetBool("Activate", true);
        //anim.Play(Animation1, -1, 0f);
        AudioSource.PlayClipAtPoint(Sound1, transform.position);
        StopAnim = true;
        isTriggered = true;
        foreach (GameObject door in Doors)
        {
            door.SendMessage("UnlockDoor");
        }

        if (isRespawner)
        {
            int i = 0;
            foreach (GameObject LookupItem in LookupObjects)
            {
                LookupItem.transform.position = ObjectPos[i];
                i++;
                if (LookupItem.CompareTag("PlatForm"))
                {
                    LookupItem.GetComponent<MoveingPlatform>().isMoving = false;
                }
            }

        }
    }


    private void OnTriggerStay(Collider other)
    {
    if (Input.GetButtonDown("Use") && isSwitch == true && StopAnim == false)
    {
        ActivateSwitch();
    }

    }
    void OnTriggerExit(Collider other)
    {
        Interactions -= 1;
        if (Interactions <= 0 && isSwitch == false) { 
        isTriggered = false;
        foreach (GameObject door in Doors)
        {
            door.SendMessage("LockDoor");
        }
            anim.SetBool("Triggered", false);
            anim.SetBool("Deactivate", true);
            //anim.Play(Animation2, -1, 0f);
        //AudioSource.PlayClipAtPoint(Sound2, transform.position);
        }
        if ((Interactions <= 0 && isSwitch == false && isEventTrigger))
        {
            isTriggered = false;
            //anim.Play(Animation1, -1, 0f);
           // AudioSource.PlayClipAtPoint(Sound1, transform.position);
            

            foreach (GameObject LookupItem in LookupObjects)
            {
               // LookupItem.GetComponent<MoveingPlatform>().isMoving = false;
                LookupItem.GetComponent<MoveingPlatform>().StartMoving();

            }



        }
        if (Interactions <= 0 && isRespawner)
        {

            foreach (GameObject LookupItem in LookupObjects)
            {
                if (LookupItem.CompareTag("PlatForm"))
                {
                    LookupItem.GetComponent<MoveingPlatform>().StartMoving();
                }
            }

        }


    }
    



}

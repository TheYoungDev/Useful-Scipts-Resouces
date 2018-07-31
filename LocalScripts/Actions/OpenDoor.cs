using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    public GameObject[] Triggers;
    public string colour = "Red";
    public AudioClip Sound1;
    public AudioClip Sound2;
    private Animator anim;
    public bool DoorClosed = true;
    public Collider m_Collider;


    void Start()
        {
            FindTriggers();
            anim = GetComponent<Animator>();
            m_Collider = GetComponent<Collider>();
        }
    public void FindTriggers()
    {


        if (colour != "None")
        {
            Triggers = GameObject.FindGameObjectsWithTag(colour + "Trigger");
        }
        if (colour == "Custom")
        {
            System.Collections.Generic.List<GameObject> temp_list = new System.Collections.Generic.List<GameObject>(Triggers);
            foreach (GameObject Trig in Triggers) {
                if(Trig.gameObject.GetComponentInParent<Material>().color != gameObject.GetComponent<Material>().color)
                {
                    print("not same colour");

                    temp_list.Remove(Trig);
                   

                }

            }
            Triggers = temp_list.ToArray();
        }
    }
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.Play("DoorOpen", -1, 0f);
            AudioSource.PlayClipAtPoint(Sound1, transform.position);
        }*/
    }

    public void UnlockDoor()
    {
        FindTriggers();
        bool allTriggered = true;
       
        foreach (GameObject Trigger in Triggers)
        {
            if (Trigger.GetComponent<TriggerEvent>()) { 
                if (Trigger.GetComponent<TriggerEvent>().isTriggered == false)
                {
                    allTriggered = false;
                }
            }
            if (Trigger.GetComponent<NetworkEventTrigger>())
            {
                if (Trigger.GetComponent<NetworkEventTrigger>().isTriggered == false)
                {
                    allTriggered = false;
                }
            }
        }

        if (allTriggered)
        {
           // m_Collider.enabled = false;
            DoorClosed = false;
            
            anim.SetBool("Open", true);
            //anim.Play("GateWayHugeDoor_Open", -1, 0f);
           AudioSource.PlayClipAtPoint(Sound1, transform.position);
           StartCoroutine("WaitForAnimation");
        }

    }
    void LockDoor()
    {
        bool allTriggered = true;
      
        foreach (GameObject Trigger in Triggers)
        {
            if (Trigger.GetComponent<TriggerEvent>())
            {
                if (Trigger.GetComponent<TriggerEvent>().isTriggered == false)
                {
                    allTriggered = false;
                }
            }
            if (Trigger.GetComponent<NetworkEventTrigger>())
            {
                if (Trigger.GetComponent<NetworkEventTrigger>().isTriggered == false)
                {
                    allTriggered = false;
                }
            }
        }
        if (!allTriggered && DoorClosed == false)
        {
            m_Collider.enabled = true;
            
            DoorClosed = true;

            // StartCoroutine("WaitForAnimation");
            anim.SetBool("Open", false);
            //StartCoroutine("WaitForAnimation");
            //anim.Play("GateWayHugeDoor_Close", -1, 0f);

            //AudioSource.PlayClipAtPoint(Sound2, transform.position);
        }

    }
    IEnumerator WaitForAnimation()
    {

        yield return new WaitForSeconds(3);
        m_Collider.enabled = false;
        print("3s");
       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lazer : MonoBehaviour
{
    public GameObject TempHit;
    private LineRenderer lr;
    public float scaler = 0f;
    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point - hit.transform.forward* scaler);
            }
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Laser") || hit.collider.gameObject.name.Contains("Laser"))
            {
                TempHit = hit.collider.gameObject;
                if (TempHit.GetComponent<RedirectLazer>())
                    hit.collider.gameObject.GetComponent<RedirectLazer>().CreateLazer();
                if (TempHit.GetComponent<NetworkEventTrigger>())
                    TempHit.GetComponent<NetworkEventTrigger>().LaserSwitchOn();
                else if (TempHit.GetComponent<TriggerEvent>())
                    TempHit.GetComponent<TriggerEvent>().LaserSwitchOn();

            }
            else if(TempHit !=null && hit.collider.gameObject != TempHit)
            {
               // Debug.Log("off"+ TempHit.name+ hit.collider.gameObject.name);
                if (TempHit.GetComponent<RedirectLazer>())
                    TempHit.GetComponent<RedirectLazer>().StopLazer();

                if(TempHit.GetComponent<NetworkEventTrigger>())
                    TempHit.GetComponent<NetworkEventTrigger>().LaserSwitchOff();
                else if (TempHit.GetComponent<TriggerEvent>())
                    TempHit.GetComponent<TriggerEvent>().LaserSwitchOff();
                TempHit = null;
            }


        }
        else lr.SetPosition(1, transform.forward * 5000);
    }
}


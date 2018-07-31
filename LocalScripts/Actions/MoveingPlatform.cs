using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour {
    public GameObject[] CheckPoints;
    public GameObject CurrentCheckPoint;
    public GameObject ColdSteam;
    public GameObject HotSteam;
    private BoxCollider Col;
    public bool isMoving =true;
    public int index = 0;
    public float speed = 5;
    //private float real_speed = 0;
    public float WaitTime = 3;
 

    // Use this for initialization
    void Start () {
        CurrentCheckPoint = CheckPoints[index];
        Col =gameObject.GetComponent<BoxCollider>();
        //isMoving = false;
        //StartMoving();
    }
    IEnumerator WaitOnStart()
    {
        print(Time.time);
        yield return new WaitForSeconds(WaitTime);
        isMoving = true;
        //real_speed = speed;
        print(Time.time);
    }
    // Update is called once per frame
    void Update () {
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, CurrentCheckPoint.transform.position, step);
        }
        if(gameObject.transform.position == CurrentCheckPoint.transform.position)
        {
            //update current checkpoint
            SwitchCheckpoint();
        }


    }
    void UpdateEffect()
    {
        if (Col.material.name.Contains("Ice") || Col.material.name.Contains("Zero"))
        {
            ColdSteam.SetActive(true);
            HotSteam.SetActive(false);
        }
        else if (Col.material.name.Contains("Bouncy"))
        {
            ColdSteam.SetActive(false);
            HotSteam.SetActive(true);
        }
        else
        {
            ColdSteam.SetActive(false);
            HotSteam.SetActive(false);
        }
    }
    public void StartMoving()
    {
        isMoving = false;
        //StartCoroutine("WaitOnStart");
        //isMoving = true;
    }
    public void FreezeMoving()
    {
        isMoving = !isMoving;
        //StartCoroutine("WaitOnStart");
        //isMoving = true;
    }
    public void SwitchCheckpoint()
    {
        index++;
        if (index == CheckPoints.Length)
        {
            index = 0;
        }
        if (CheckPoints[index].gameObject.activeInHierarchy)
        {
            CurrentCheckPoint = CheckPoints[index];
        }

    }
    public void OnTriggerEnter(Collider other)
    {

    }
}

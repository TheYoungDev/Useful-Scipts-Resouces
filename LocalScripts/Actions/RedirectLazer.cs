using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectLaser : MonoBehaviour
{
    public GameObject[] LaserSources;
    public GameObject Shield;
    public GameObject Crystal;
    public Material OffMat;
    public Material OnMat;
    public int CurrentLaserIndex;
    Animator anim;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateLaser()
    {
        //Debug.Log();
        anim = GetComponent<Animator>();
        if(anim)
            anim.SetBool("Trigger", true);
        Crystal.GetComponent<Renderer>().material = OnMat;
        LaserSources[CurrentLaserIndex].SetActive(true);
        Shield.SetActive(true);
    }
    public void StopLaser()
    {
        // Debug.Log(Direction);
        foreach (GameObject Laser in LaserSources)
        {
            Laser.SetActive(false);
        }
        Shield.SetActive(false);
        Crystal.GetComponent<Renderer>().material = OffMat;
        if (anim)
            anim.SetBool("Trigger", false);

    }
}

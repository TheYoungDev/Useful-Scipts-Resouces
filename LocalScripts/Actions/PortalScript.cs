using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {

    public GameObject[] OtherPortal;
    public string Colour;
	// Use this for initialization
	void Start () {
        //OtherPortal = GameObject.FindGameObjectsWithTag(Colour + "Portal");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (OtherPortal.Length <= 1) { 
            OtherPortal = GameObject.FindGameObjectsWithTag(Colour + "Portal");
            //print("test");
        }
        if (other.tag=="Player")
        {
            other.transform.position = OtherPortal[0].transform.position + OtherPortal[0].transform.forward;
            other.transform.rotation = OtherPortal[0].transform.rotation;
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotColdEffect : MonoBehaviour {
    public GameObject ColdSteam;
    public GameObject HotSteam;
    private BoxCollider Col;

    // Use this for initialization
    void Start () {
        Col = gameObject.GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateEffect()
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
}

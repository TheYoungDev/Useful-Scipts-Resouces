using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerKill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<playerNormalMovement>().Death();
        }
    }
}

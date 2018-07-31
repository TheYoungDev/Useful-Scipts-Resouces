using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
       //play death sound
            other.gameObject.GetComponent<playerNormalMovement>().Death();
        } 
    }
}

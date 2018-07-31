using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    public float Force;
    private Rigidbody Rb;
    public GameObject CollidedObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        CollidedObject = other.transform.gameObject;
        Rb = CollidedObject.GetComponent<Rigidbody>();
        Rb.AddForce(transform.up * Force);



    }
}

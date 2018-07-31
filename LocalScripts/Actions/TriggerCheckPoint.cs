using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckPoint : MonoBehaviour {
    public bool wasTriggered = false;
    public bool CheckPoint = true;
    public GameObject[] CheckPoints;
    public int GiveAbility = -1;
    public bool SinglePlayer = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        wasTriggered = true;

        if(other.gameObject.CompareTag("Player") && GiveAbility != -1)
        {
            other.gameObject.GetComponent<PlayerObjectController>().AllowedAbilitiesBool[GiveAbility] = true;
            other.gameObject.GetComponent<PlayerObjectController>().SwicthAbility(GiveAbility);
        }
        if (other.gameObject.CompareTag("Player") && CheckPoint)
        {
            if(!SinglePlayer)
                other.gameObject.GetComponent<playerNormalMovement>().CheckPoint = gameObject;
            else
                other.gameObject.GetComponent<LocalPlayerMovement>().CheckPoint = gameObject;
        }

    }
}

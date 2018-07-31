using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkDisable : NetworkBehaviour
{
    public GameObject[] DisableObjects;
    public MonoBehaviour[]  DisableComponents;
    // Use this for initialization
    void Start () {
        // Disable_Component()
       // Disable_Component();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void DestroyObjects()
    {

    }
    public void Disable_Objects()
    {
        foreach(GameObject item in DisableObjects)
        {
            item.SetActive(false);
        }
    }
    public void Disable_Component()
    {
        foreach (MonoBehaviour item in DisableComponents)
        {
            item.enabled = false;
        }
    }
}

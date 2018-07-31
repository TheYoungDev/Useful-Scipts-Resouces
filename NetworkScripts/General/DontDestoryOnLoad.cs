using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryOnLoad : MonoBehaviour {
    private bool Flag = false;
    public GameObject[] DontDestroyObjects;
    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        if (!Flag)
        {
            foreach (GameObject item in DontDestroyObjects)
            {
                DontDestroyOnLoad(item.gameObject);

            }
            DontDestroyOnLoad(this.gameObject);
            Flag = true;
            //Debug.Log("Awake: " + this.gameObject);

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAbility : MonoBehaviour {

    public GameObject BluePortal;
    public GameObject RedPortal;
    public GameObject Blue_Portal;
    public GameObject Red_Portal;
    public bool shotRed = false;
    public bool shotBlue = false;
    public Camera mainCamera;
    public bool isEnabled = false;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
       // mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        if (isEnabled) {
        if (Input.GetButtonDown("Fire1"))
        {

            
            ShootPortal(true);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            
            ShootPortal(false);
        }
        }
    }
    void ShootPortal(bool blue)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Wall") || hit.transform.gameObject.CompareTag("PlatForm")) {
                if (!shotBlue && blue)
                {
                    Debug.Log("blue portal");
                    shotBlue = true;
                    Blue_Portal = Instantiate(BluePortal, hit.point, Quaternion.LookRotation(hit.normal));
                }
                if (!shotRed && !blue)
                {
                    Debug.Log("red portal");
                    shotRed = true;
                    Red_Portal = Instantiate(RedPortal, hit.point, Quaternion.LookRotation(hit.normal));
                }
                if (shotRed && !blue)
                {
                    Red_Portal.transform.rotation = Quaternion.LookRotation(hit.normal);
                    Red_Portal.transform.position = hit.point;
                }
                if (shotBlue && blue)
                {
                    Blue_Portal.transform.rotation = Quaternion.LookRotation(hit.normal);
                    Blue_Portal.transform.position = hit.point;
                }
                //Debug.Log(Quaternion.LookRotation(hit.normal));

                // Quaternion rotation = Quaternion.LookRotation(hit.normal); //Rotation;
                //rotation *= Quaternion.Euler(0, 90, 0); // this adds a 90 degrees Y rotation
                //Portal.transform.rotation = Quaternion.Slerp(Portal.transform.rotation, rotation, Time.deltaTime * 1);
            }
        }
    }
}

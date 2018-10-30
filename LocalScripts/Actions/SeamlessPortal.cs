using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamlessPortal : MonoBehaviour {


    public Transform PlayerCamera;
    public Transform PortalA;
    public Transform PortalB;
    public Transform Pointer;
    public Vector3 OldPos;
    public bool flag = false;
    public bool GroundPortal = false;


    void Start () {
        OldPos = PortalA.position;

    }
	
	// Update is called once per frame
	void Update () {
        if (OldPos != PortalA.position)
        {
            OldPos = PortalA.position;
            flag = false;
        }

        if (PlayerCamera == null)
        {
            
            GameObject Player = GameObject.FindGameObjectWithTag("Player");

            if(Player !=null)
                PlayerCamera = Player.transform.GetChild(1);
        }


        if(PlayerCamera != null && PortalA != null && PortalB != null) { 
            Vector3 playerOffset = PlayerCamera.position - PortalB.position;
            float angularDiff = Quaternion.Angle(PortalA.rotation, PortalB.rotation);
            angularDiff += 180;
            Quaternion portalAngleDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
            Vector3 newCamDirection = portalAngleDiff * PlayerCamera.forward;

            Pointer.rotation =  Quaternion.LookRotation(newCamDirection, Vector3.up);
            float LocalRotAngleY = Pointer.transform.localRotation.y;
            float LocalRotAngleX = Pointer.transform.localRotation.x;
            float LocalRotAngleZ = Pointer.transform.localRotation.z;
            Debug.Log(PortalA.gameObject.name + " Rotation:  " + PortalA.rotation.x);
            if (Mathf.Abs(PortalA.rotation.x) >= 0.3)
            {
                GroundPortal = true;
                
            }
            else
            {
                GroundPortal = false;
            }
            if (Mathf.Abs(LocalRotAngleY) <= 0.7 && Mathf.Abs(LocalRotAngleX) <= 0.7 && !GroundPortal)
            {
                transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
            }

            else if (Mathf.Abs(LocalRotAngleY) >= 0.7 && !GroundPortal)
            {
                angularDiff = Quaternion.Angle(PortalA.rotation, PortalB.rotation);
                portalAngleDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
                newCamDirection = portalAngleDiff * PlayerCamera.forward;
                transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
            }
            else if(GroundPortal)
            {
                angularDiff = Quaternion.Angle(PortalA.rotation, PortalB.rotation);
                Debug.Log("Check");
                portalAngleDiff = Quaternion.AngleAxis(angularDiff, Vector3.forward);
                newCamDirection = portalAngleDiff * PlayerCamera.up;
                transform.rotation = PortalA.transform.rotation;

            }



        }
        
    }
}

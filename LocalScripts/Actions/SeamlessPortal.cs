using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamlessPortal : MonoBehaviour {


    public Transform PlayerCamera;
    public Transform PortalA;//closest to player?
    public Transform PortalB;
    public Transform Pointer;
    public Vector3 OldPos;
    public bool flag = false;
    public bool GroundPortal = false;
    //public Transform OtherPortal;
    // Use this for initialization
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
        /**/
        if (PlayerCamera == null)
        {
            //Debug.Log("test");
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            //Debug.Log(Player.name);
            if(Player !=null)
                PlayerCamera = Player.transform.GetChild(1);
        }


        if(PlayerCamera != null && PortalA != null && PortalB != null) { 
            Vector3 playerOffset = PlayerCamera.position - PortalB.position;
            //transform.position = PortalA.position + playerOffset;
            float angularDiff = Quaternion.Angle(PortalA.rotation, PortalB.rotation);
            angularDiff += 180;
           // Debug.Log(Quaternion.Angle(PortalA.rotation, PortalB.rotation));
            Quaternion portalAngleDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
            Vector3 newCamDirection = portalAngleDiff * PlayerCamera.forward;
            //newCamDirection.y = Mathf.Clamp(newCamDirection.y, -0.5f, 0.5f);
            

           // Quaternion tempRot = Quaternion.LookRotation(newCamDirection, Vector3.up);
            // tempRot.eulerAngles.y = Mathf.Clamp(transform.eulerAngles.y, -90, 90);
            //float RotAngle = PortalA.forward.y-gameObject.transform.forward.y;

            Pointer.rotation =  Quaternion.LookRotation(newCamDirection, Vector3.up);
            float LocalRotAngleY = Pointer.transform.localRotation.y;
            float LocalRotAngleX = Pointer.transform.localRotation.x;
            float LocalRotAngleZ = Pointer.transform.localRotation.z;
            Debug.Log(PortalA.gameObject.name + "Hello  " + PortalA.rotation.x);
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
                //angularDiff = 180;
                // Debug.Log(Quaternion.Angle(PortalA.rotation, PortalB.rotation));
                portalAngleDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
                newCamDirection = portalAngleDiff * PlayerCamera.forward;
                transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
            }
            else if(GroundPortal)
            {

                angularDiff = Quaternion.Angle(PortalA.rotation, PortalB.rotation);
                //angularDiff += 180;
                Debug.Log("Check");
                portalAngleDiff = Quaternion.AngleAxis(angularDiff, Vector3.forward);
                newCamDirection = portalAngleDiff * PlayerCamera.up;
                //transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.forward);
                transform.rotation = PortalA.transform.rotation;

            }
           
            //Debug.Log(PortalA.gameObject.name +"LocalRotAngleX " + LocalRotAngleX+ "LocalRotAngleZ " + LocalRotAngleZ);
            //transform.eulerAngles.y = Mathf.Clamp(transform.eulerAngles.y, -90, 90);

            //Vector3 currentRotation = transform.localRotation.eulerAngles;
            //currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);
            // transform.localRotation = Quaternion.Euler(currentRotation);


        }
        
    }
}
//fix this

//
/* if (Vector3.Angle(PortalA.transform.forward, transform.forward) >= 100 && Vector3.Angle(PortalA.transform.forward, transform.forward) <= 260 && (angularDiff<=100 || angularDiff >= 80) || flag)
 {

     angularDiff -= 180;
     flag = true;

 }*/


/* if (Vector3.Angle(PortalA.transform.forward, transform.forward) >= 100 && Vector3.Angle(PortalA.transform.forward, transform.forward) <= 260 && (angularDiff<=100 || angularDiff >= 80) || flag)
{

angularDiff -= 180;
flag = true;

}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPortal : MonoBehaviour {

    public GameObject[] OtherPortal;
    public Material[] RenderMaterials;
    public RenderTexture[] RendeTextures;
    public string Colour;
    private bool flag = false;
    public float thrust = 200;
    public Vector3 GravityDirection;
    public int direction;
    public Material CameraMat;
    public Camera Camera;
    public bool SinglePlayer = true;
    // Use this for initialization
    void Start()
    {
        GravityDirection = new Vector3(0, 1, 0);
        //OtherPortal = GameObject.FindGameObjectsWithTag(Colour + "Portal");
        if (Camera != null)
        {
            if (OtherPortal.Length == 0)
            {
                OtherPortal = GameObject.FindGameObjectsWithTag(Colour + "Portal");
                //print("test");
            }
            SetPortalTecture();
        }

    }

    public void SetPortalTecture()
    {

        Camera = OtherPortal[0].transform.GetChild(0).gameObject.GetComponent<Camera>();
        OtherPortal[0].transform.GetChild(0).gameObject.GetComponent<SeamlessPortal>().PortalB = gameObject.transform;
        //if (Camera == null)
        //return;

        if (Camera.targetTexture != null)
        {
            Camera.targetTexture.Release();
        }
        Camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        CameraMat.mainTexture = Camera.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (OtherPortal.Length > 0)
        {
            if (Camera == null)
            {
                SetPortalTecture();
            }
        }
        if (Colour == "")
            return;

        if (OtherPortal.Length == 0)
        {
            OtherPortal = GameObject.FindGameObjectsWithTag(Colour + "Portal");
            //print("test");
        }

        if (flag)
            return;





        for (int i =0; i  < RenderMaterials.Length; i++)
        {
            if (RenderMaterials[i].name.Contains(Colour)){
                gameObject.GetComponent<Renderer>().material = RenderMaterials[i];
                CameraMat = RenderMaterials[i];
                /*if(Colour  == "Red"|| Colour == "Blue" || Colour == "Green" || Colour == "Yellow")
                    gameObject.transform.GetChild(0).GetComponent<Camera>().targetTexture = RendeTextures[i+1];
                if (Colour == "Pink" || Colour == "Purple" || Colour == "Cyan" || Colour == "Orange")
                    gameObject.transform.GetChild(0).GetComponent<Camera>().targetTexture = RendeTextures[i -1];*/
                flag = true;
                break;
            }
        }
        




    }
    /* IEnumerable EnableGravity(GameObject temp)
      {
          yield return new WaitForSeconds(5);


      }
     void OnCollisionExit(Collision collision)
     {
         Debug.Log("yeas");
         collision.gameObject.GetComponent<playerNormalMovement>().GravityDisabled = false;
         StartCoroutine("EnableGravity()", 
         collision.gameObject.GetComponent<playerNormalMovement>().CanJump = true;
     }*/
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!SinglePlayer)
                other.gameObject.GetComponent<playerNormalMovement>().CanJump = false;
            else
                other.gameObject.GetComponent<LocalPlayerMovement>().CanJump = false;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!SinglePlayer) { 
                other.gameObject.GetComponent<playerNormalMovement>().GravityDisabled = true;
                other.gameObject.GetComponent<playerNormalMovement>().CanJump = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                other.gameObject.GetComponent<LocalPlayerMovement>().GravityDisabled = true;
                other.gameObject.GetComponent<LocalPlayerMovement>().CanJump = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (!SinglePlayer)
            {
                other.gameObject.GetComponent<playerNormalMovement>().GravityDisabled = false;
                other.gameObject.GetComponent<playerNormalMovement>().CanJump = true;
            }
            else
            {
                other.gameObject.GetComponent<LocalPlayerMovement>().GravityDisabled = false;
                other.gameObject.GetComponent<LocalPlayerMovement>().CanJump = true;
            }
        }
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        thrust = col.impulse.magnitude*30+200;
        if (col.gameObject.tag == "PickUp")
        {
            thrust = col.impulse.magnitude*30 + 200;
        }
        if(thrust >= 4000)
        {
            thrust = 4000;
        }
       // Debug.Log(col.impulse.magnitude*30+200);
        if (OtherPortal.Length <= 1)
        {
            OtherPortal = GameObject.FindGameObjectsWithTag(Colour + "Portal");
            //print("test");
        }
        if (OtherPortal.Length == 0 || OtherPortal[0] == null)
        {
            return;
            //print("test");
        }
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "PickUp" )
        {
            if (GravityDirection == new Vector3(0, 1, 0))
            {

            col.gameObject.transform.position = OtherPortal[0].transform.position + OtherPortal[0].transform.forward;


                //Check if gravitydirection matches normal of portal
                if (col.gameObject.tag == "Player")
                {
                    if(!SinglePlayer)
                        GravityDirection = col.gameObject.GetComponent<playerNormalMovement>().GravityRotation;
                    else
                        GravityDirection = col.gameObject.GetComponent<LocalPlayerMovement>().GravityRotation;
                }
                //gravity is normal, portal is rotated)
            
                if(OtherPortal[0].transform.rotation.x >= 210 && OtherPortal[0].transform.rotation.x <= 350) { 
                    if(col.gameObject.tag == "Player")
                    {
                        if (!SinglePlayer)
                        {
                            col.gameObject.GetComponent<playerNormalMovement>().GravityDisabled = true;
                            col.gameObject.GetComponent<playerNormalMovement>().CanJump = false;
                        }
                        else
                        {
                            col.gameObject.GetComponent<LocalPlayerMovement>().GravityDisabled = true;
                            col.gameObject.GetComponent<LocalPlayerMovement>().CanJump = false;
                        }
                    }
                    if (col.gameObject.tag == "PickUp")
                    {
                        col.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    }

                        //
                        //decrease gravity?
                        //WaitToEnable
                }
                Debug.Log(OtherPortal[0].transform.rotation.y);
                if (Mathf.Abs(OtherPortal[0].transform.rotation.y) > 0.55)
                {
                    Debug.Log("Rotatedt");
                    col.gameObject.transform.rotation = OtherPortal[0].transform.rotation;
                }
            }
                // col.gameObject.transform.rotation = OtherPortal[0].transform.rotation;
                //keep players Y rotation
           
            else if (GravityDirection == new Vector3(1, 0, 0))
            {

                col.gameObject.transform.position = OtherPortal[0].transform.position + OtherPortal[0].transform.forward;


                //Check if gravitydirection matches normal of portal
                if (col.gameObject.tag == "Player")
                {
                    if(!SinglePlayer)
                        GravityDirection = col.gameObject.GetComponent<playerNormalMovement>().GravityRotation;
                    else
                        GravityDirection = col.gameObject.GetComponent<LocalPlayerMovement>().GravityRotation;
                }
                //gravity is normal, portal is rotated)
               // Debug.Log(OtherPortal[0].name+" yes siur " + OtherPortal[0].transform.rotation.y);
                if (Mathf.Abs(OtherPortal[0].transform.rotation.y) >= 0.6 )
                {
                    if (col.gameObject.tag == "Player")
                    {
                        if (!SinglePlayer) { 
                            col.gameObject.GetComponent<playerNormalMovement>().GravityDisabled = true;
                            col.gameObject.GetComponent<playerNormalMovement>().CanJump = false;
                        }
                        else
                        {
                            col.gameObject.GetComponent<LocalPlayerMovement>().GravityDisabled = true;
                            col.gameObject.GetComponent<LocalPlayerMovement>().CanJump = false;
                        }
                    }
                    if (col.gameObject.tag == "PickUp")
                    {
                        col.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    }

                }

                if (Mathf.Abs(OtherPortal[0].transform.rotation.y) < 0.6)
                {
                    //Debug.Log("yes siur");
                    col.gameObject.transform.rotation = OtherPortal[0].transform.rotation;
                }
                else
                {
                    //col.gameObject.transform.rotation = new Quaternion(col.gameObject.transform.rotation.x, OtherPortal[0].transform.rotationn.y, col.gameObject.transform.rotation.z, col.gameObject.transform.rotation.w);
                }
                // col.gameObject.transform.rotation = OtherPortal[0].transform.rotation;
                //keep players Y rotation
            }
            //if(col.gameObject.transform.rotation.y >=0)

            col.gameObject.GetComponent<Rigidbody>().AddForce(OtherPortal[0].transform.forward * thrust);
            //col.gameObject.GetComponent<Rigidbody>().AddForce(col.gameObject.transform.forward * thrust);

        }   
        //col.relativeVelocity;
        // myRB.AddForce(-50 * mass * direction);
    }

    /*void OnTriggerEnter(Collider other)
    {

        
        if (OtherPortal.Length <= 1)
        {
            OtherPortal = GameObject.FindGameObjectsWithTag(Colour + "Portal");
            //print("test");
        }
        Debug.Log(other.gameObject.name);
        if (other.tag == "Player" || other.tag == "PickUp")
        {
            other.transform.position = OtherPortal[0].transform.position + OtherPortal[0].transform.forward;
            other.GetComponent<Rigidbody>().AddForce(OtherPortal[0].transform.forward * thrust);


            switch (direction)
            {
                case 0:
                    Debug.Log("Change to gravity normal");
                    //GravityDirection = new Vector3(0, 0, 0);
                    //RotateCharacter(GravityRotation);
                    //other.transform.rotation = OtherPortal[0].transform.rotation.y;
                    other.transform.rotation = OtherPortal[0].transform.rotation;
                    break;
                case 1:
                    Debug.Log("Change to gravity upwards");

                    //GravityDirection = new Vector3(0, 0, 180f);
                    // RotateCharacter(GravityRotation);
                    break;
                case 2:
                    Debug.Log("Change to gravity leftside");

                    //GravityDirection = new Vector3(0, 0, -90);
                    // RotateCharacter(GravityRotation);
                    break;
                case 3:
                    Debug.Log("Change to gravity rightside");

                    //GravityDirection = new Vector3(0, 0, 90);
                    // RotateCharacter(GravityRotation);
                    break;
                case 4:
                    Debug.Log("Change to gravity forward");

                    //GravityDirection = new Vector3(-90, 0, 0);
                    // RotateCharacter(GravityRotation);
                    break;
                case 5:
                    Debug.Log("Change to gravity backwards");
                    //GravityDirection = new Vector3(90, 0, 0);
                    //RotateCharacter(GravityRotation);
                    break;

            }
        }
    }*/
}

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPortalAbility : NetworkBehaviour
{
    public int Count =0;
    public string[] Colours = { "Red", "Pink", "Blue", "Purple", "Green", "Cyan", "Yellow","Orange" };  //Orange, Purple
    public string SelectedColour1 = "Black";
    public string SelectedColour2 = "White";
    public Color[] ColourCodes = { new Color(255, 0, 0, 255), new Color(255, 105, 180, 255), new Color(0, 0, 255, 255), new Color(128, 0, 128, 255), new Color(0, 128, 0, 255), new Color(0, 255, 255, 255), new Color(255, 255, 0, 255), new Color(255, 165, 0, 255) };
    public Color ColourCode1;
    public Color ColourCode2;
    public Material[] RenderMaterials;
    public RenderTexture[] RendeTextures;
    public GameObject Portal1;
    public GameObject Portal2;
    public GameObject _Portal1;
    public GameObject _Portal2;
    public bool shotPortal1 = false;
    public bool shotPortal2 = false;
    public Camera mainCamera;
    private MonoBehaviour PlayerMovementScript;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer == false)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<playerNormalMovement>().PausedGame == false)
        {

            //Debug.Log(SelectedColour + " portal");
            ShootPortal(true);
        }
        if (Input.GetButtonDown("Fire2") && gameObject.GetComponent<playerNormalMovement>().PausedGame == false)
        {
            //Debug.Log(SelectedColour + " portal");
            ShootPortal(false);
        }
    }
    void ShootPortal(bool blue)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Wall") || hit.transform.gameObject.CompareTag("PlatForm"))
            {
                if (!shotPortal1 && blue)
                {
                    int Count = 0;
                    for (int i = 0; i < Colours.Length; i += 2) {
                        if (SelectedColour1 != "Black")
                            break;
                        string TempString1= Colours[i] + "Portal";
                        string TempString2 = Colours[i+1] + "Portal";
                        if (GameObject.FindGameObjectWithTag(TempString1))
                            Debug.Log(GameObject.FindGameObjectWithTag(TempString1).name);
                        if (GameObject.FindGameObjectWithTag(TempString1) != null || GameObject.FindGameObjectWithTag(TempString2) != null)
                        {

                            //continue to loop
                        }
                        else
                        {
                           // if (GameObject.FindGameObjectsWithTag(TempString1) != null)
                                //Debug.Log("test" + (GameObject.FindGameObjectWithTag(TempString1).name));
                            SelectedColour1 = Colours[i];
                            SelectedColour2 = Colours[i + 1];
                            ColourCode1 = ColourCodes[i];
                            ColourCode2 = ColourCodes[i + 1];
                            Count = i;
                            break;
                        }
                        
                    }
                    Vector3 TempPos = new Vector3(hit.point.x-250, hit.point.x-250, hit.point.z - 250);
                    //1st time shot
                    shotPortal1 = true;
                    
                    //_Portal1 = Instantiate(Portal1, hit.point, Quaternion.LookRotation(hit.normal));
                    /* NetworkServer.SpawnWithClientAuthority(_Portal1, connectionToClient);*/
                    // _Portal1 = Instantiate(Portal1, _Pos, _Rot);
                    Debug.Log(SelectedColour1);
                    CmdSpawnPortal(true, SelectedColour1, SelectedColour2, ColourCode1,hit.point, Quaternion.LookRotation(hit.normal));
                    if(!isServer)
                        _Portal1 = GameObject.FindGameObjectWithTag(SelectedColour1 + "Portal");
                    //CmdChangeColour(_Portal1, SelectedColour, Colours[i +1], ColourCode);
                    //CmdUpdateTransform(_Portal1, hit.point, Quaternion.LookRotation(hit.normal));
                    
                    //check if player has used portals of your colour


                    //wait then display portal
                    //StartCoroutine(Delay(_Portal1, hit.point));

                }
                if (!shotPortal2 && !blue)
                {
                    int Count = 1;
                    for (int i = 1; i< Colours.Length-1; i+=2)
                    {
                        if (SelectedColour1 != "Black")
                            break;
                        string TempString1 = Colours[i] + "Portal";
                        string TempString2 = Colours[i - 1] + "Portal";
                        if(GameObject.FindGameObjectWithTag(TempString1))
                            Debug.Log(GameObject.FindGameObjectWithTag(TempString1).name);
                        if (GameObject.FindGameObjectWithTag(TempString1) != null || GameObject.FindGameObjectWithTag(TempString2) != null)
                        {
                            //continue loop
                        }
                        else
                        {

                            SelectedColour1 = Colours[i-1];
                            SelectedColour2 = Colours[i];
                            ColourCode1 = ColourCodes[i-1];
                            ColourCode2 = ColourCodes[i];
                            Count = i;
                            break;
                        }
                        //i += 2;
                    }

                    Vector3 TempPos = new Vector3(hit.point.x - 250, hit.point.x - 250, hit.point.z - 250);
                    //1st time shot
                    shotPortal2 = true;
                    /*
                    if (_Portal2 == null)
                        Debug.Log(Portal2.name);
                    else
                        Debug.Log(_Portal2.name);*/
                    
                    // NetworkServer.SpawnWithClientAuthority(_Portal2, connectionToClient);
                    CmdSpawnPortal(false, SelectedColour2, SelectedColour1, ColourCode2, hit.point, Quaternion.LookRotation(hit.normal));
                    if (!isServer) {
                        Debug.Log("Testing1");
                        _Portal2 = GameObject.FindGameObjectWithTag(SelectedColour2 + "Portal");
                    }
                    //_Portal2 = GameObject.FindGameObjectWithTag(SelectedColour + "Portal");
                    Debug.Log(SelectedColour2);
                    //CmdChangeColour(_Portal2, SelectedColour, Colours[i - 1], ColourCode);
                    //CmdUpdateTransform(_Portal2, hit.point, Quaternion.LookRotation(hit.normal));
                    //StartCoroutine(Delay(_Portal2, hit.point));
                    //Red_Portal = Instantiate(RedPortal, hit.point, Quaternion.LookRotation(hit.normal));
                }
                if (shotPortal2 && !blue)
                {
                    //_Portal2.transform.rotation = Quaternion.LookRotation(hit.normal);
                    //_Portal2.transform.position = hit.point;
                    _Portal2 = GameObject.FindGameObjectWithTag(SelectedColour2 + "Portal");
                    if (_Portal2 != null)
                        CmdUpdateTransform(_Portal2, hit.point, Quaternion.LookRotation(hit.normal));
                }
                if (shotPortal1 && blue)
                {
                    //_Portal1.transform.rotation = Quaternion.LookRotation(hit.normal);
                    //_Portal1.transform.position = hit.point;
                    _Portal1 = GameObject.FindGameObjectWithTag(SelectedColour1 + "Portal");
                    if (_Portal1 != null)
                        CmdUpdateTransform(_Portal1, hit.point, Quaternion.LookRotation(hit.normal));
                }
                //Debug.Log(Quaternion.LookRotation(hit.normal));

                // Quaternion rotation = Quaternion.LookRotation(hit.normal); //Rotation;
                //rotation *= Quaternion.Euler(0, 90, 0); // this adds a 90 degrees Y rotation
                //Portal.transform.rotation = Quaternion.Slerp(Portal.transform.rotation, rotation, Time.deltaTime * 1);
            }
        }
    }
    [Command]
    void CmdSpawnPortal(bool _PortalOne,string _Colour1, string _Colour2, Color _ColourCode, Vector3 _Pos, Quaternion _Rot)
    {
        //Debug.Log("??");
        GameObject _NetworkPortal; 
        if (_PortalOne) { 
           
            _NetworkPortal = Instantiate(Portal1, _Pos, _Rot);
            _Portal1 = _NetworkPortal;
        }
        else
        {
            _NetworkPortal = Instantiate(Portal2, _Pos, _Rot);
            _Portal2 = _NetworkPortal;
        }
        /*Debug.Log(_NetworkPortal.name);
        Debug.Log(_Colour1);
        if (_NetworkPortal == null)
            _NetworkPortal = _Portal1;*/
        NetworkServer.SpawnWithClientAuthority(_NetworkPortal, connectionToClient);


        _NetworkPortal.tag = _Colour1 + "Portal";
        _NetworkPortal.GetComponent<NetworkPortal>().Colour = _Colour2;
        _NetworkPortal.transform.GetChild(1).GetComponent<ParticleSystem>().startColor = _ColourCode;
        RpcChangeColour(_NetworkPortal, _Colour1, _Colour2, _ColourCode);
        

        _NetworkPortal.transform.position = _Pos;
        _NetworkPortal.transform.rotation = _Rot;
        RpcUpdateTransform(_NetworkPortal, _Pos, _Rot);
    }

    [Command]
    void CmdChangeColour(GameObject _PortalObject, string _Colour1, string _Colour2, Color _ColourCode)
    {
        _PortalObject.tag = _Colour1 + "Portal";
        _PortalObject.GetComponent<NetworkPortal>().Colour = _Colour2;
        _PortalObject.transform.GetChild(1).GetComponent<ParticleSystem>().startColor = _ColourCode;
        RpcChangeColour(_PortalObject, _Colour1, _Colour2, _ColourCode);
        Debug.Log(_Colour1);
    }
    [ClientRpc]
    void RpcChangeColour(GameObject _PortalObject, string _Colour1, string _Colour2, Color _ColourCode)
    {
        _PortalObject.tag = _Colour1 + "Portal";
        _PortalObject.GetComponent<NetworkPortal>().Colour = _Colour2;
        _PortalObject.transform.GetChild(1).GetComponent<ParticleSystem>().startColor = _ColourCode;
        Debug.Log(_Colour1);
    }
    [Command]
    void CmdUpdateTransform(GameObject _PortalObject, Vector3 _Pos, Quaternion _Rot)
    {
        _PortalObject.transform.position = _Pos;
        _PortalObject.transform.rotation = _Rot;
        RpcUpdateTransform(_PortalObject, _Pos, _Rot);
    }
    [ClientRpc]
    void RpcUpdateTransform(GameObject _PortalObject, Vector3 _Pos, Quaternion _Rot)
    {
        _PortalObject.transform.position = _Pos;
        _PortalObject.transform.rotation = _Rot;
       
    }
    IEnumerator Delay(GameObject _Portal, Vector3 _pos)
    {
        yield return new WaitForSeconds(2);

        _Portal.transform.position = _pos;



    }

}

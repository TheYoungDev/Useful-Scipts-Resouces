using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMindControl : MonoBehaviour {
    public bool Enabled = false;
    GameObject PlayerObject;
    public float speed =1f;
    public Animator anim;
    public SkinnedMeshRenderer MatObject;
    public Material[] NormalMat;
    public Material[] MindControlledMat;
    // Use this for initialization
    void Start () {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Enabled)
        {
            if (PlayerObject.GetComponent<LocalPlayerMovement>().MindControl)
            {
                Debug.Log("hor" + speed * Input.GetAxis("Horizontal") * Time.deltaTime);
                Debug.Log("vert" +speed * Input.GetAxis("Vertical") * Time.deltaTime);
                transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, speed * Input.GetAxis("Vertical") * Time.deltaTime);
                if (Mathf.Abs(Input.GetAxis("Vertical")) >=0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.1f)
                {
                    anim.SetBool("Pathing", true);
                }
                else
                {
                    anim.SetBool("Pathing", false);
                }
            }
                
        }
       

	}
    public void UpdateEnabled(bool temp_bool)
    {
        Enabled = temp_bool;
        if (temp_bool)
        {
            MatObject.materials = MindControlledMat;
        }
        else
        {
            MatObject.materials = NormalMat;
        }
    }
}

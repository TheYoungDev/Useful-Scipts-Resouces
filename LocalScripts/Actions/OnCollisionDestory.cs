using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDestory : MonoBehaviour {

    public string FireType = "Fire";
    public PhysicMaterial BouncyMat;
    public PhysicMaterial IceMat;
    public PhysicMaterial NormalMat;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Shoot(GameObject Col)
    {
        if (FireType == "None")
            return;
        
           
            //float shootForce = 500;
            if (Col.gameObject.GetComponent<BoxCollider>())
        {
            if (FireType == "Fire")
            {
                if (Col.gameObject.tag.Contains("Enemy"))
                {

                }
                if (Col.gameObject.CompareTag("PlatForm"))
                {
                    if (Col.gameObject.GetComponent<BoxCollider>().material.name.Contains(NormalMat.name))
                    {
                        Col.gameObject.GetComponent<BoxCollider>().material = BouncyMat;
                        Debug.Log(Col.gameObject.GetComponent<BoxCollider>().material.name);
                    }
                    if (Col.gameObject.GetComponent<BoxCollider>().material.name.Contains(IceMat.name))
                    {
                        Col.gameObject.GetComponent<BoxCollider>().material = NormalMat;
                    }
                }
                if (Col.gameObject.CompareTag("Wall"))
                {
                    if (Col.gameObject.GetComponent<BoxCollider>().material.name.Contains(NormalMat.name))
                    {
                        Col.gameObject.GetComponent<BoxCollider>().material = BouncyMat;
                    }
                    if (Col.gameObject.GetComponent<BoxCollider>().material == IceMat)
                    {
                        Col.gameObject.GetComponent<BoxCollider>().material = NormalMat;
                    }
                }

                if (Col.gameObject.GetComponent<HotColdEffect>())
                {
                    Col.gameObject.GetComponent<HotColdEffect>().UpdateEffect();
                
            }

            /*GameObject go = Instantiate(FireBall, mainCamera.transform.GetChild(0).position, mainCamera.transform.GetChild(0).rotation);
            go.GetComponent<Rigidbody>().AddForce(mainCamera.transform.GetChild(0).forward * shootForce);*/
        }
        if (FireType == "Ice")
        {
            
                Debug.Log(Col.gameObject.name);
                if (Col.gameObject.tag.Contains("Enemy"))
                {

                }
                if (Col.gameObject.CompareTag("PlatForm"))
                {
                    if (Col.gameObject.GetComponent<BoxCollider>().material.name.Contains(NormalMat.name))
                    {
                        Col.gameObject.GetComponent<BoxCollider>().material = IceMat;
                    }
                    if (Col.gameObject.GetComponent<BoxCollider>().material.name.Contains(BouncyMat.name))
                    {
                        Col.gameObject.GetComponent<BoxCollider>().material = NormalMat;
                    }
                }
                if (Col.gameObject.CompareTag("Wall"))
                {
                    if (Col.gameObject.GetComponent<BoxCollider>().material.name.Contains(NormalMat.name))
                    {
                        Col.gameObject.GetComponent<BoxCollider>().material = IceMat;
                    }
                    if (Col.gameObject.GetComponent<BoxCollider>().material.name.Contains(BouncyMat.name))
                    {
                        Col.gameObject.GetComponent<BoxCollider>().material = NormalMat;
                    }
                }
            
            /* GameObject go = Instantiate(IceBall, mainCamera.transform.GetChild(0).position, mainCamera.transform.GetChild(0).rotation);
             go.GetComponent<Rigidbody>().AddForce(mainCamera.transform.GetChild(0).forward * shootForce);*/
            }
            if (Col.gameObject.GetComponent<HotColdEffect>())
            {
                Col.gameObject.GetComponent<HotColdEffect>().UpdateEffect();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Shoot(collision.transform.gameObject);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}

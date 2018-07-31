using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {

    public Texture2D crosshairTexture;
    public float crosshairScale = 1;
    void OnGUI()
    {
        //if not paused
        if (Time.timeScale != 0)
        {
            if (crosshairTexture != null)
                GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width * crosshairScale) / 2, (Screen.height - crosshairTexture.height * crosshairScale) / 2, crosshairTexture.width * crosshairScale, crosshairTexture.height * crosshairScale), crosshairTexture);
            else
                Debug.Log("No crosshair texture set in the Inspector");
        }
    }
    private void Update()
    {
        //if (Input.GetKey(KeyCode.Escape))
            //Screen.lockCursor = false;
       // else
            //Screen.lockCursor = true;//*/
    
    }
}

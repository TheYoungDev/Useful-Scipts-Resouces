using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerScript : MonoBehaviour {
    public Text TimeText;
    //private float startTime;

	// Use this for initialization
	void Start () {
       // startTime = Time.unscaledTime;
	}
	
	// Update is called once per frame
	void Update () {
        TimeText.text = "Time Played: " + Mathf.Round( Time.timeSinceLevelLoad/60) + "m "+ (Mathf.Round(Time.timeSinceLevelLoad)-Mathf.Round(Time.timeSinceLevelLoad / 60)) + "s ";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunction : MonoBehaviour {
    public int num=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TestConsole()
    {
        Debug.Log("TestConsole");
    }
    public void TestConsoleInt(int i)
    {
        num = i;
        Debug.Log("TestConsoleInt " + i);
    }
}

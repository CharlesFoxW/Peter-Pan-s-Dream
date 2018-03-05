using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsQuit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void isQuit(bool quit)
    {
        if (quit)
        {
            Application.Quit();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

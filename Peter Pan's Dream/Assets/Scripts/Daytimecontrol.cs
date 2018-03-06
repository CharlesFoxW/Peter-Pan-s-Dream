using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daytimecontrol : MonoBehaviour {
    
    public static float timepast = 0f;
    private float timebar = 10f;

    // Use this for initialization
    void Start()
    {
        timepast = 0f;
    }
	
    // Update is called once per frame
    void Update()
    {
        timepast += Time.deltaTime;
        if (!PlayerController.isDead && timepast >= timebar)
        {
            Debug.Log("inchange");
            timepast = 0f;
            SceneControl.Instance.LoadScene1();
        }
    }
}

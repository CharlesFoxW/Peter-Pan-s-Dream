using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daytimecontrol : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        timepast = 0f;
    }
    private float timepast = 0f;
    private float timebar = 2f;
    // Update is called once per frame
    void Update()
    {
        timepast += Time.deltaTime;
        if (timepast >= timebar)
        {
            Debug.Log("inchange");
            SceneControl.Instance.LoadScene1();
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daytimecontrol : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        timepast = 0f;
    }
    private float timepast = 0f;
    public float timebar = 10f;
    // Update is called once per frame
    void Update()
    {
        timepast += Time.deltaTime;
        if (timepast >= timebar)
        {
            Debug.Log("inchange");
            timepast = 0f;
            SceneControl.Instance.LoadScene1();
        }
    }
}
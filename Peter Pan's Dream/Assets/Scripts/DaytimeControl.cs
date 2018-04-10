using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeControl : MonoBehaviour {

    public GameObject myFade;
    public static float timepast = 0f;
    private float timebar = 30f;

    private ScoreManager myScoreManager;

    // Use this for initialization
    void Start()
    {
        timepast = 0f;
        myScoreManager = FindObjectOfType<ScoreManager>();
    }
	
    // Update is called once per frame
    void Update()
    {
        timepast += Time.deltaTime;
    }

}

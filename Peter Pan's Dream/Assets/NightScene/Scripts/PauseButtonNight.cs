using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonNight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                PauseuiNight.GameIsPaused = true;
            });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

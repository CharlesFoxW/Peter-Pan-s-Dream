using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gamebegin : MonoBehaviour {

	// Use this for initialization
	void Start () {

        this.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            SceneControl.Instance.daybegin();
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

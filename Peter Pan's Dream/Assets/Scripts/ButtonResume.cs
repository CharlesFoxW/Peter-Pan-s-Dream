using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonResume : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(delegate ()
			{
				Time.timeScale = 1;
			});
	}

	// Update is called once per frame
	void Update () {

	}
}

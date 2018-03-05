using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class stop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(delegate ()
			{
				Time.timeScale = 0;
			});
	}

	// Update is called once per frame
	void Update () {

	}
}

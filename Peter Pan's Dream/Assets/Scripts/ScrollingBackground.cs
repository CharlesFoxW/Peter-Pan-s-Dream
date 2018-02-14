using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public CameraController cc;
	public GameObject background2;

	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		if (cc.transform.position.x >= this.transform.position.x) {
			background2.transform.position = new Vector3 (
				this.transform.position.x + this.transform.localScale.x,
				background2.transform.position.y,
				background2.transform.position.z
			);
		}
		
	}
}

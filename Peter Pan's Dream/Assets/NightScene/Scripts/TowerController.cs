using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {


    private float x;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x <= GameControl.instance.Boundary_LEFT) {
            transform.localScale = new Vector3(1f, 1f, 1f);
            x = transform.position.x;
            transform.position = new Vector3(x, -0.5f, 0);
        }
	}
}

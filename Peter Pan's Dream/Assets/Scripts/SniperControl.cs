using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperControl : MonoBehaviour {

	public GameObject bullet;

	public float waitBetweenShots;
	private float shotCounter;
	private Vector3 firePointOffset;

	// Use this for initialization
	void Start () {
		firePointOffset = new Vector3 (-0.8f, -0.5f, 0f);
		shotCounter = waitBetweenShots;
	}
	
	// Update is called once per frame
	void Update () {

		shotCounter -= Time.deltaTime;

		if (shotCounter < 0) {
			Instantiate (bullet, transform.position + firePointOffset, Quaternion.AngleAxis (180, Vector3.up));
			shotCounter = waitBetweenShots;
		}


	}
}

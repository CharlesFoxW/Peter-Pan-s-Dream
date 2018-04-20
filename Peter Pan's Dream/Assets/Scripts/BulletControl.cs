using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

	public float flyingSpeed;

	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		myRigidbody.velocity = new Vector2 (-flyingSpeed, 0f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player") {
			Debug.Log ("Got shot!");
		}


	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyMovement : MonoBehaviour {
    private Rigidbody2D rb2d;
    private float fairyYSpeed = -3f;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, fairyYSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y >= 2)
        {
            rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, fairyYSpeed); 
        }
        if (transform.position.y <= 0)
        {
            rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, -fairyYSpeed); 
        }
	}
}
